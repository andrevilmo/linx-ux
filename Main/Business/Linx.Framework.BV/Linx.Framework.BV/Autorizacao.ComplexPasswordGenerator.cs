using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Linx.Framework.BV.Autorizacao
{
    /// <summary>
    /// Generates and validates passwords aligned with SPA rules
    /// (min 12 chars, upper/lower/digit/special, no sequential digits).
    /// </summary>
    public static class ComplexPasswordGenerator
    {
        private const int DefaultLength = 14;
        private const int MaxAttempts = 100;

        private static readonly string Upper = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        private static readonly string Lower = "abcdefghijkmnopqrstuvwxyz";
        private static readonly string Digits = "23456789";
        // Same special set as SPA UsuarioAutorizacaoContext password regex.
        private static readonly string Special = "~!@#$%^&*+-/.,\\{}[]();:?<>\"'_";
        private static readonly string All = Upper + Lower + Digits + Special;

        private static readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return EmailRegex.IsMatch(email.Trim());
        }

        public static bool MeetsComplexity(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 12)
                return false;
            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit))
                return false;
            if (!password.Any(c => Special.IndexOf(c) >= 0))
                return false;
            foreach (char c in password)
            {
                if (char.IsLetterOrDigit(c))
                    continue;
                if (Special.IndexOf(c) < 0)
                    return false;
            }
            return !HasSequentialNumbers(password, 3);
        }

        public static string Generate(int length = DefaultLength)
        {
            if (length < 12)
                length = 12;

            for (int attempt = 0; attempt < MaxAttempts; attempt++)
            {
                string password = BuildCandidate(length);
                if (MeetsComplexity(password))
                    return password;
            }

            throw new InvalidOperationException("Nao foi possivel gerar uma senha que atenda as regras de complexidade.");
        }

        private static string BuildCandidate(int length)
        {
            var chars = new List<char>(length)
            {
                Pick(Upper),
                Pick(Lower),
                Pick(Digits),
                Pick(Special)
            };

            while (chars.Count < length)
                chars.Add(Pick(All));

            return new string(Shuffle(chars).ToArray());
        }

        private static char Pick(string alphabet)
        {
            return alphabet[NextInt(alphabet.Length)];
        }

        private static IEnumerable<char> Shuffle(IList<char> chars)
        {
            for (int i = chars.Count - 1; i > 0; i--)
            {
                int j = NextInt(i + 1);
                char tmp = chars[i];
                chars[i] = chars[j];
                chars[j] = tmp;
            }
            return chars;
        }

        private static int NextInt(int exclusiveMax)
        {
            if (exclusiveMax <= 0)
                throw new ArgumentOutOfRangeException("exclusiveMax");
            byte[] bytes = new byte[4];
            using (var rng = new RNGCryptoServiceProvider())
            {
                uint value;
                uint limit = uint.MaxValue - (uint.MaxValue % (uint)exclusiveMax);
                do
                {
                    rng.GetBytes(bytes);
                    value = BitConverter.ToUInt32(bytes, 0);
                } while (value >= limit);
                return (int)(value % (uint)exclusiveMax);
            }
        }

        public static bool HasSequentialNumbers(string value, int sequenceLength = 3)
        {
            if (string.IsNullOrEmpty(value) || value.Length < sequenceLength)
                return false;

            for (int i = 0; i <= value.Length - sequenceLength; i++)
            {
                string slice = value.Substring(i, sequenceLength);
                bool allDigits = true;
                for (int k = 0; k < slice.Length; k++)
                {
                    if (!char.IsDigit(slice[k]))
                    {
                        allDigits = false;
                        break;
                    }
                }
                if (!allDigits)
                    continue;

                bool ascending = true;
                bool descending = true;
                for (int j = 1; j < slice.Length; j++)
                {
                    int diff = slice[j] - slice[j - 1];
                    if (diff != 1)
                        ascending = false;
                    if (diff != -1)
                        descending = false;
                }
                if (ascending || descending)
                    return true;
            }
            return false;
        }
    }
}
