using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Linx.Security
{
    /// <summary>
    ///  Classe Linx para criptografia.
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Cryptography
    {
        /// <summary>
        /// Variável interna para o vetor de inicialização.
        /// </summary>
        private string _Key;

        /// <summary>
        /// Vetor de inicialização. 
        /// </summary>
        /// <value>Deve ser preenchida com os 8 primeiros caracteres do CNPJ do cliente.</value>
        public string Key
        {
            get
            {
                return _Key.Length != 0 ? this._Key : strBaseKey;
            }
            set
            {

                if (value.Length > 0 && value.Length != 8)
                    throw new InvalidDataException("A chave deve conter 8 caracteres.");

                _Key = value;
            }
        }

        /// <summary>
        /// Variável interna para utilizar o seed.
        /// </summary>
        private bool _UseSeed = true;

        /// <summary>
        /// Utiliza seed para criptografia. 
        /// Se for verdadeiro, cada chamada à função Encrypt gerará um texto criptografado diferente.
        /// </summary>
        public bool UseSeed
        {
            get
            {
                return _UseSeed;
            }
            set
            {
                _UseSeed = value;
            }
        }

        /// <summary>
        /// Chave para geração da criptografia. O ideal seria dificultar a abertura dessa chave.
        /// Se for usado um decompilador, nossa segurança fica comprometida.
        /// </summary>
        private const string strCryptoKey = "*(}$Linx&#%$Sistemas!{Aurelia$#SaoPaulo*&Brasil#$*)*";

        /// <summary>
        /// Código base de criptografia. Será utilizado esse código se a propriedade Key estiver vazia.
        /// </summary>
        private const string strBaseKey = "54517628";

        /// <summary>
        /// Construtor com parâmetro da chave.
        /// </summary>
        /// <param name="strKey">Chave de criptografia</param>
        public Cryptography(string strKey)
        {
            this.Key = strKey;
        }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public Cryptography()
        {
            this._Key = "";
        }

        /// <summary>
        /// Criptografa uma string e retorna a representação base64 da criptografia.
        /// Utiliza Encrypt(byte[], byte[], byte[]) 
        /// </summary>
        /// <param name="strClearText">Texto para criptografar</param>
        /// <returns>Representação base64 da criptografia de strClearText</returns>
        public string Encrypt(string strClearText)
        {
            if (this.Key == "")
                return "";

            byte[] btClear = System.Text.Encoding.Unicode.GetBytes(strClearText);

            byte[] btSalt = new byte[3] { 0x01, 0x02, 0x03 };

            if (this._UseSeed)
            {
                RNGCryptoServiceProvider rngSalt = new RNGCryptoServiceProvider();
                rngSalt.GetBytes(btSalt);
            }

            PasswordDeriveBytes pdbCrypto = new PasswordDeriveBytes(strCryptoKey + strBaseKey + this.Key, btSalt);

            byte[] btEncrypted = Encrypt(btClear, pdbCrypto.GetBytes(32), Encoding.Default.GetBytes(strBaseKey + this.Key));

            return Convert.ToBase64String(btSalt) + Convert.ToBase64String(btEncrypted);
        }

        /// <summary>
        /// Criptografa um array usando uma chave e um vetor de inicialização. O algoritmo utilizado é o Rijndael.
        /// </summary>
        /// <param name="btClearData">Array com o texto para criptografar</param>
        /// <param name="btKey">Array de 32 bytes com a chave de criptografia</param>
        /// <param name="btIV">Array de 16 bytes com o vetor de inicialização</param>
        /// <returns>Array com o texto criptografado</returns>
        public byte[] Encrypt(byte[] btClearData, byte[] btKey, byte[] btIV)
        {
            MemoryStream msCrypto = new MemoryStream();

            Rijndael algCrypto = Rijndael.Create();

            algCrypto.Key = btKey;
            algCrypto.IV = btIV;

            CryptoStream csCrypto = new CryptoStream(msCrypto, algCrypto.CreateEncryptor(), CryptoStreamMode.Write);

            csCrypto.Write(btClearData, 0, btClearData.Length);

            csCrypto.Close();

            byte[] btEncryptedData = msCrypto.ToArray();

            return btEncryptedData;
        }

        /// <summary>
        /// Descriptografa uma string criptografada e representada em base64
        ///	Utiliza Decrypt(byte[], byte[], byte[])
        /// </summary>
        /// <param name="strCipherText">Texto criptografado</param>
        /// <returns>Texto descriptografado</returns>
        public string Decrypt(string strCipherText)
        {
            if (this.Key == "")
                return "";

            byte[] btSalt = Convert.FromBase64String(strCipherText.Substring(0, 4));
            byte[] btCipher = Convert.FromBase64String(strCipherText.Substring(4));

            PasswordDeriveBytes pdbCrypto = new PasswordDeriveBytes(strCryptoKey + strBaseKey + this.Key, btSalt);

            byte[] btDecrypted;

            try
            {
                btDecrypted = Decrypt(btCipher, pdbCrypto.GetBytes(32), Encoding.Default.GetBytes(strBaseKey + this.Key));
            }
            catch
            {
                return "";
            }

            return System.Text.Encoding.Unicode.GetString(btDecrypted);
        }

        /// <summary>
        /// Descriptografa um array usando uma chave e um vetor de inicialização. O algoritmo utilizado é o Rijndael.
        /// </summary>
        /// <param name="btCipherData">Array com o texto criptografado</param>
        /// <param name="btKey">Array de 32 bytes com a chave de criptografia</param>
        /// <param name="btIV">Array de 16 bytes com o vetor de inicialização</param>
        /// <returns>Array com o texto descriptografado</returns>
        public byte[] Decrypt(byte[] btCipherData, byte[] btKey, byte[] btIV)
        {
            MemoryStream msCrypto = new MemoryStream();

            Rijndael algCrypto = Rijndael.Create();

            algCrypto.Key = btKey;
            algCrypto.IV = btIV;

            CryptoStream csCrypto = new CryptoStream(msCrypto, algCrypto.CreateDecryptor(), CryptoStreamMode.Write);

            csCrypto.Write(btCipherData, 0, btCipherData.Length);

            csCrypto.Close();

            byte[] btDecryptedData = msCrypto.ToArray();

            return btDecryptedData;
        }
    }
}
