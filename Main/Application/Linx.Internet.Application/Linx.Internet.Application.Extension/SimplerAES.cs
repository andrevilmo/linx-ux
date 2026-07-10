// -----------------------------------------------------------------------
// <copyright file="SimplerAES.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Internet.Application
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SimplerAES
    {
        /// <summary>
        /// chave de bytes
        /// </summary>
        private static byte[] key = { 12, 215, 19, 11, 24, 28, 85, 54, 141, 184, 27, 162, 37, 102, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 121, 236, 53, 209 };

        /// <summary>
        /// vector de bytes
        /// </summary>
        private static byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 221, 112, 79, 32, 114, 156, 44 };

        /// <summary>
        /// implementação de interface
        /// </summary>
        private ICryptoTransform encryptor, decryptor;

        /// <summary>
        /// codificação em UTF8Encoding 
        /// </summary>
        private UTF8Encoding encoder;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimplerAES" /> class.
        /// </summary>
        public SimplerAES()
        {
            RijndaelManaged rm = new RijndaelManaged();
            this.encryptor = rm.CreateEncryptor(key, vector);
            this.decryptor = rm.CreateDecryptor(key, vector);
            this.encoder = new UTF8Encoding();
        }

        /// <summary>
        /// Criptografar string
        /// </summary>
        /// <param name="unencrypted">texto para ser criptografado</param>
        /// <returns>retorna string criptografada</returns>
        public string Encrypt(string unencrypted)
        {
            return Convert.ToBase64String(this.Encrypt(this.encoder.GetBytes(unencrypted)));
        }

        /// <summary>
        /// Descriptografar string
        /// </summary>
        /// <param name="encrypted">texto criptografado</param>
        /// <returns>retorna string descriptografada</returns>
        public string Decrypt(string encrypted)
        {
            return this.encoder.GetString(this.Decrypt(Convert.FromBase64String(encrypted)));
        }

        /// <summary>
        /// Criptografa URL
        /// </summary>
        /// <param name="unencrypted">string url</param>
        /// <returns>retorna url criptografada</returns>
        public string EncryptToUrl(string unencrypted)
        {
            return HttpUtility.UrlEncode(this.Encrypt(unencrypted));
        }

        /// <summary>
        /// DesCriptografa URL
        /// </summary>
        /// <param name="encrypted">string url criptografada</param>
        /// <returns>retorna url descriptografada</returns>
        public string DecryptFromUrl(string encrypted)
        {
            return this.Decrypt(HttpUtility.UrlDecode(encrypted));
        }

        /// <summary>
        /// criptografar bytes
        /// </summary>
        /// <param name="buffer">bytes a serem criptografados</param>
        /// <returns>retorna bytes</returns>
        public byte[] Encrypt(byte[] buffer)
        {
            return this.Transform(buffer, this.encryptor);
        }

        /// <summary>
        /// Descriptografa bytes
        /// </summary>
        /// <param name="buffer">bytes para descriptografar</param>
        /// <returns>retorna bytes</returns>
        public byte[] Decrypt(byte[] buffer)
        {
            return this.Transform(buffer, this.decryptor);
        }

        /// <summary>
        /// Executa algoritimo com bytes
        /// </summary>
        /// <param name="buffer">criptografia bytes </param>
        /// <param name="transform">interface ICryptoTransform</param>
        /// <returns>retorna bytes</returns>
        protected byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                {
                    cs.Write(buffer, 0, buffer.Length);
                }

                return stream.ToArray();
            }
        }
    }
}
