using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;
using System.Reflection;
using System.Security.Cryptography;
using System.IO.Compression;

namespace Linx.Business.Common
{
    public class Util
    {
        public static Dictionary<string, byte> UF = new Dictionary<string, byte>()
        {          
            {"RO", 11},
            {"AC", 12},
            {"AM", 13},
            {"RR", 14},
            {"PA", 15},
            {"AP", 16},
            {"TO", 17},
            {"MA", 21},
            {"PI", 22},
            {"CE", 23},
            {"RN", 24},
            {"PB", 25},
            {"PE", 26},
            {"AL", 27},
            {"SE", 28},
            {"BA", 29},
            {"MG", 31},
            {"ES", 32},
            {"RJ", 33},
            {"SP", 35},
            {"PR", 41},
            {"SC", 42},
            {"RS", 43},
            {"MS", 50},
            {"MT", 51},
            {"GO", 52},
            {"DF", 53}        
        };

        public static string SubstituiCaracteresEspeciais(string texto)
        {
            return texto.Replace("\n", "")
                        .Replace("\t", "")
                        .Replace("ª", "")
                        .Replace("º", "RO")
                        .Replace("°", "RO")
                        .Replace("  ", " ")
                        .Replace("<", "")
                        .Replace("''", "")
                        .Replace("*", "")
                        .Replace("%", "")
                        .Replace("&amp;", "E")
                        .Replace("&", "E")
                        .Replace("&Ccedil;", "C")
                        .Replace("Ç", "C")
                        .Replace("&Atilde;", "A")
                        .Replace("Ã", "A")
                        .Replace("&Otilde;", "O")
                        .Replace("Õ", "O")
                        .Replace("&Aacute;", "A")
                        .Replace("Á", "A")
                        .Replace("&Eacute;", "E")
                        .Replace("É", "E")
                        .Replace("&Iacute;", "I")
                        .Replace("Í", "I")
                        .Replace("&Oacute;", "O")
                        .Replace("Ó", "O")
                        .Replace("Ü", "U")
                        .Replace("&Uacute;", "U")
                        .Replace("Ú", "U")
                        .Replace("&Ecirc;", "E")
                        .Replace("Ê", "E")
                        .Replace("&Acirc;", "A")
                        .Replace("Â", "A")
                        .Replace("&Ocirc;", "O")
                        .Replace("Ô", "O")
                        .Replace("?", "")
                        .Replace("!", "")
                        .Replace("~", "")
                        .Replace("&Agrave;", "A")
                        .Replace("À", "A")
                        .Replace("È", "E")
                        .Replace("Ì", "I")
                        .Replace("Ò", "O")
                        .Replace("Ù", "U")
                        .Replace("Ñ", "N")
                        .Replace("'", " ");
        }

        public static string FormataValor(object valor, int casasDecimais)
        {
            if (valor == null)
            {
                return null;
            }

            return String.Format(System.Globalization.CultureInfo.GetCultureInfo("en-US"), "{0:0." + "".PadLeft(casasDecimais, '0') + "}", valor);
        }

        public static string RetiraMascaraTelefone(string telefone)
        {
            return telefone.Replace("(", "").Replace(")", "").Replace("-", "").Trim();
        }

        public static string RetiraFormatacaoDocumentos(string documento)
        {
            return documento.Replace(".", "").Replace("/", "").Replace("-", "").Replace(",", "").Trim();
        }

        /// <summary>
        /// Método busca o valor de um objeto decimal refletido do "nome de um campo" e converte para o valor do objeto        
        /// </summary>
        /// <param name="o"></param>
        /// <param name="nomeCampo"></param>
        /// <returns></returns>
        public static decimal ConvertNomeCampoToObjeto(object o, string nomeCampo)
        {
            try
            {
                var p = o.GetType().GetProperties().FirstOrDefault(n => n.Name == nomeCampo);
                decimal ret = (decimal)p.GetValue(o);
                return ret;
            }
            catch
            {
                return 0;
            }
        }

        public static string TrataErroWebException(WebException webException)
        {
            string responseError = string.Empty;
            if (!webException.Response.IsNullOrEmpty())
            {
                using (var reader = new StreamReader(webException.Response.GetResponseStream()))
                {
                    responseError = reader.ReadToEnd();
                }
                responseError = responseError.Replace("\"", string.Empty);

                if (responseError.Contains("ErrorMessage:"))
                {
                    responseError = responseError.Extract("ErrorMessage:", ",IsDomainException");
                }
                else if (responseError.Contains("ExceptionMessage:"))
                {
                    responseError = responseError.Extract("ExceptionMessage:", ",ExceptionType");
                }
                else if (responseError.Contains("<Fault"))
                {
                    responseError = responseError.Extract("<Message>", "</Message>");
                }
                else if (responseError.ToLower().Contains("<html>"))
                {
                    responseError = null;
                }
            }
            else
            {
                responseError = webException.Message.ToString();

                responseError = responseError.Replace("\"", string.Empty);
                if (responseError.Contains("DomainException:"))
                {
                    responseError = responseError.Extract("System.ServiceModel.DomainServices.Server.DomainException: ", "at");
                }
            }

            return responseError.IsNullOrEmpty() ? webException.Message.ToString() : responseError;
        }

        public static string EncodeToHexadecimal(string value)
        {
            string hex = "";

            foreach (char c in value)
            {
                hex += ((int)c).ToString("x");
            }
            return hex;
        }

        /// <summary>
        /// Converte String para base 64
        /// </summary>
        /// <param name="toEncode"></param>
        /// <returns>string base 64</returns>
        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes
                  = System.Text.ASCIIEncoding.UTF8.GetBytes(toEncode);
            string returnValue
                  = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        /// <summary>
        /// Convert string para SHA1
        /// </summary>
        /// <param name="str"></param>
        /// <returns>string com SHA1</returns>
        public static string EncodeToSHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));

            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Compactar string para o formato Zip 
        /// </summary>
        /// <param name="toEncode"></param>
        /// <returns>texto compactado no formato base 64</returns>
        public static string EncodeToZIP(string toEncode)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(toEncode);

            MemoryStream ms = new MemoryStream();
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            byte[] bb = ms.ToArray();
            ms.Position = 0;

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            return Convert.ToBase64String(bb);
        }

        /// <summary>
        /// Descompactar zip para o formato string 
        /// </summary>
        /// <param name="binario"></param>
        /// <returns>string base 64 descompactada</returns>
        public static string DecodeFromZIP(string binario)
        {
            string resultado = string.Empty;
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(binario)))
            {
                using (GZipStream zs = new GZipStream(ms, CompressionMode.Decompress))
                {
                    using (StreamReader st = new StreamReader(zs))
                    {
                        resultado = st.ReadToEnd();
                    }
                }
            }
            return resultado;
        }

        public static string Descriptografa(string valor)
        {
            string cipherText = string.Empty;

            if (String.IsNullOrEmpty(valor))
                throw new Exception("O valor a ser descriptografado está nulo ou em branco.");

            try
            {
                string passPhrase = "Pas5pr@se";        // can be any string
                string saltValue = "s@1tValue";        // can be any string
                string hashAlgorithm = "SHA1";             // can be "MD5"
                int passwordIterations = 2;                  // can be any number
                string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
                int keySize = 256;                // can be 192 or 128

                cipherText = RijndaelSimple.Decrypt(valor,
                                                            passPhrase,
                                                            saltValue,
                                                            hashAlgorithm,
                                                            passwordIterations,
                                                            initVector,
                                                            keySize);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha na hora de criptografar o valor informado.", ex);
            }
            return cipherText;
        }


        public static string Criptografa(string valor)
        {
            string cipherText = string.Empty;

            if (String.IsNullOrEmpty(valor))
                throw new Exception("O valor a ser criptografado está nulo ou em branco.");

            try
            {
                string passPhrase = "Pas5pr@se";        // can be any string
                string saltValue = "s@1tValue";        // can be any string
                string hashAlgorithm = "SHA1";             // can be "MD5"
                int passwordIterations = 2;                  // can be any number
                string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
                int keySize = 256;                // can be 192 or 128

                cipherText = RijndaelSimple.Encrypt(valor,
                                                            passPhrase,
                                                            saltValue,
                                                            hashAlgorithm,
                                                            passwordIterations,
                                                            initVector,
                                                            keySize);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha na hora de criptografar o valor informado.", ex);
            }
            return cipherText;
        }

        public class RijndaelSimple
        {
            /// <summary>
            /// Encrypts specified plaintext using Rijndael symmetric key algorithm
            /// and returns a base64-encoded result.
            /// </summary>
            /// <param name="plainText">
            /// Plaintext value to be encrypted.
            /// </param>
            /// <param name="passPhrase">
            /// Passphrase from which a pseudo-random password will be derived. The
            /// derived password will be used to generate the encryption key.
            /// Passphrase can be any string. In this example we assume that this
            /// passphrase is an ASCII string.
            /// </param>
            /// <param name="saltValue">
            /// Salt value used along with passphrase to generate password. Salt can
            /// be any string. In this example we assume that salt is an ASCII string.
            /// </param>
            /// <param name="hashAlgorithm">
            /// Hash algorithm used to generate password. Allowed values are: "MD5" and
            /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
            /// </param>
            /// <param name="passwordIterations">
            /// Number of iterations used to generate password. One or two iterations
            /// should be enough.
            /// </param>
            /// <param name="initVector">
            /// Initialization vector (or IV). This value is required to encrypt the
            /// first block of plaintext data. For RijndaelManaged class IV must be 
            /// exactly 16 ASCII characters long.
            /// </param>
            /// <param name="keySize">
            /// Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
            /// Longer keys are more secure than shorter keys.
            /// </param>
            /// <returns>
            /// Encrypted value formatted as a base64-encoded string.
            /// </returns>
            public static string Encrypt(string plainText,
                                            string passPhrase,
                                            string saltValue,
                                            string hashAlgorithm,
                                            int passwordIterations,
                                            string initVector,
                                            int keySize)
            {
                // Convert strings into byte arrays.
                // Let us assume that strings only contain ASCII codes.
                // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
                // encoding.
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                // Convert our plaintext into a byte array.
                // Let us assume that plaintext contains UTF8-encoded characters.
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                // First, we must create a password, from which the key will be derived.
                // This password will be generated from the specified passphrase and 
                // salt value. The password will be created using the specified hash 
                // algorithm. Password creation can be done in several iterations.
                PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                                passPhrase,
                                                                saltValueBytes,
                                                                hashAlgorithm,
                                                                passwordIterations);

                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).
                byte[] keyBytes = password.GetBytes(keySize / 8);

                // Create uninitialized Rijndael encryption object.
                RijndaelManaged symmetricKey = new RijndaelManaged();

                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = CipherMode.CBC;

                // Generate encryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                                    keyBytes,
                                                                    initVectorBytes);

                // Define memory stream which will be used to hold encrypted data.
                MemoryStream memoryStream = new MemoryStream();

                // Define cryptographic stream (always use Write mode for encryption).
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                                encryptor,
                                                                CryptoStreamMode.Write);
                // Start encrypting.
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                // Finish encrypting.
                cryptoStream.FlushFinalBlock();

                // Convert our encrypted data from a memory stream into a byte array.
                byte[] cipherTextBytes = memoryStream.ToArray();

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Convert encrypted data into a base64-encoded string.
                string cipherText = Convert.ToBase64String(cipherTextBytes);

                // Return encrypted string.
                return cipherText;
            }

            /// <summary>
            /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
            /// </summary>
            /// <param name="cipherText">
            /// Base64-formatted ciphertext value.
            /// </param>
            /// <param name="passPhrase">
            /// Passphrase from which a pseudo-random password will be derived. The
            /// derived password will be used to generate the encryption key.
            /// Passphrase can be any string. In this example we assume that this
            /// passphrase is an ASCII string.
            /// </param>
            /// <param name="saltValue">
            /// Salt value used along with passphrase to generate password. Salt can
            /// be any string. In this example we assume that salt is an ASCII string.
            /// </param>
            /// <param name="hashAlgorithm">
            /// Hash algorithm used to generate password. Allowed values are: "MD5" and
            /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
            /// </param>
            /// <param name="passwordIterations">
            /// Number of iterations used to generate password. One or two iterations
            /// should be enough.
            /// </param>
            /// <param name="initVector">
            /// Initialization vector (or IV). This value is required to encrypt the
            /// first block of plaintext data. For RijndaelManaged class IV must be
            /// exactly 16 ASCII characters long.
            /// </param>
            /// <param name="keySize">
            /// Size of encryption key in bits. Allowed values are: 128, 192, and 256.
            /// Longer keys are more secure than shorter keys.
            /// </param>
            /// <returns>
            /// Decrypted string value.
            /// </returns>
            /// <remarks>
            /// Most of the logic in this function is similar to the Encrypt
            /// logic. In order for decryption to work, all parameters of this function
            /// - except cipherText value - must match the corresponding parameters of
            /// the Encrypt function which was called to generate the
            /// ciphertext.
            /// </remarks>
            public static string Decrypt(string cipherText,
                                            string passPhrase,
                                            string saltValue,
                                            string hashAlgorithm,
                                            int passwordIterations,
                                            string initVector,
                                            int keySize)
            {
                // Convert strings defining encryption key characteristics into byte
                // arrays. Let us assume that strings only contain ASCII codes.
                // If strings include Unicode characters, use Unicode, UTF7, or UTF8
                // encoding.
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                // Convert our ciphertext into a byte array.
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                // First, we must create a password, from which the key will be 
                // derived. This password will be generated from the specified 
                // passphrase and salt value. The password will be created using
                // the specified hash algorithm. Password creation can be done in
                // several iterations.
                PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                                passPhrase,
                                                                saltValueBytes,
                                                                hashAlgorithm,
                                                                passwordIterations);

                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).
                byte[] keyBytes = password.GetBytes(keySize / 8);

                // Create uninitialized Rijndael encryption object.
                RijndaelManaged symmetricKey = new RijndaelManaged();

                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = CipherMode.CBC;

                // Generate decryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                                    keyBytes,
                                                                    initVectorBytes);

                // Define memory stream which will be used to hold encrypted data.
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

                // Define cryptographic stream (always use Read mode for encryption).
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                                decryptor,
                                                                CryptoStreamMode.Read);

                // Since at this point we don't know what the size of decrypted data
                // will be, allocate the buffer long enough to hold ciphertext;
                // plaintext is never longer than ciphertext.
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                // Start decrypting.
                int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                            0,
                                                            plainTextBytes.Length);

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Convert decrypted data into a string. 
                // Let us assume that the original plaintext string was UTF8-encoded.
                string plainText = Encoding.UTF8.GetString(plainTextBytes,
                                                            0,
                                                            decryptedByteCount);

                // Return decrypted string.   
                return plainText;
            }


        }
    }
}
