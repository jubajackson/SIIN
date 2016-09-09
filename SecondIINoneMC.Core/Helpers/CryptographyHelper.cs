#region using directives
using System;
using System.Security.Cryptography;
using System.IO;
using Microsoft.Win32;
using Debug = System.Diagnostics.Debug;
#endregion

namespace SecondIINoneMC.Core.Helpers
{
    [Serializable]
    public static class CryptographyHelper
    {
        #region Constants

        #region Private Constants

        private const Int32 _rsaFullProvider = 1;
        private const String _providerName = "Microsoft Strong Cryptographic Provider";
        private const String _containerName = "jitu";
        private const String _key = "SOFTWARE\\jitu";
        private const String _publicKeyValue = "PublicKey";
        private const String _privateKeyValue = "PrivateKey";

        #endregion

        #endregion

        #region Member Variables

        #region Private Members

        private static String _publicKey = String.Empty;
        private static String _privateKey = String.Empty;

        #endregion

        #endregion

        #region Constructors

        #region Static Constructors

        static CryptographyHelper()
        {
            Initialize();
        }

        #endregion

        #endregion

        #region Properties

        #region Private Properties

        /// <summary>
        /// Property that returns the Public Key that's used to encrypt.
        /// </summary>
        /// <value>Public key that's being used to encrypt data.</value>
        private static String PublicKey
        {
            get
            {
                //if (_publicKey == null || _publicKey.Trim().Length == 0)
                //{
                //    RegistryKey key = Registry.LocalMachine.OpenSubKey(_key);


                //    if (key != null)
                //    {
                //        try
                //        {
                //            _publicKey = (String)key.GetValue(_publicKeyValue);
                //        }
                //        catch (System.InvalidCastException)
                //        {
                //            throw new SecurityException(StringResources.ConfigurationInvalidCast);
                //        }
                //    }
                //}

                //return _publicKey;

                return @"<RSAKeyValue><Modulus>suxWBBR2JpudguMKRl7vrNvWBG1oOeEfs9tfcQYhEHFUKrVMVdC6plgtAVfkNXbMpb4+By573l4P/J2NvVH8wjdG9mv4GM7kn6GRaJVotQmghs0tdQ9R5hyusmZhE3qRThinnqgV48xSs/CE8EWBhKWrgeq05RVU/5u/XWV4jlM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            }
        }

        /// <summary>
        /// Property that returns the Private Key that's used to decrypt.
        /// </summary>
        /// <value>Private key that's being used to decrypt data.</value>
        private static String PrivateKey
        {
            get
            {
                return @"<RSAKeyValue><Modulus>suxWBBR2JpudguMKRl7vrNvWBG1oOeEfs9tfcQYhEHFUKrVMVdC6plgtAVfkNXbMpb4+By573l4P/J2NvVH8wjdG9mv4GM7kn6GRaJVotQmghs0tdQ9R5hyusmZhE3qRThinnqgV48xSs/CE8EWBhKWrgeq05RVU/5u/XWV4jlM=</Modulus><Exponent>AQAB</Exponent><P>3xQKVhIbTvupxbKWF2sTNUtPKKwOaJde8viBYZTbMGgDRJhhSX72ZSsuTNLUo/bR4MlsBqvXuvDY3VKbA+tyhw==</P><Q>zVQalN/f+ERSuobpas8dPEBsaH6fzztZzGpGtIQoI5wTeBTcWFCA1ofwiMAWa9hZHDmqpqL5md7FiRxBGvic1Q==</Q><DP>upsjAljnKyzuGYiIcVKKoNw8fYNgEjH9pTYn1J8Ws+luQmVNjJN+Pabfdjgu1HHVozoa+YGqVqC+aHV/YsoD5Q==</DP><DQ>CoB6u5KXygL0mEW+OpBNVn+VC+MuGVNXzHTDGQiJZQjelg27F2lGrbJWQyhP/UQbiq4IZG2BhrU2NUUUr2R34Q==</DQ><InverseQ>AQB3CFfMj74QzQirghGhtrxAHtUwnFmX3FXY4kfN1jsf9pTXc7K8SryIjppC2d4VSHDpvmag6xEU1riPyNNHqQ==</InverseQ><D>BpAsx5GIk/n5WSfC5LnuyMR3oUnyHM1pQEu3wImdBOFjWP21gyBY+GkbnYT/1gY5k4ra86B84rdIltegr2UmhFmTutOkyciD/YfnlaEif7KkcKAO6gRVofBeLIYtXopLe9Bf6tc0Fh9n+yw13IMPu8I4C1cBO5BAVESEY3mXUOE=</D></RSAKeyValue>";
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Static Private Methods

        /// <summary>
        /// Initialize the CspParameters with the container and provider and flags.
        /// </summary>
        private static void Initialize()
        {
            //CspParameters cspParams;

            //cspParams = new CspParameters(_rsaFullProvider);
            //cspParams.KeyContainerName = _containerName;
            //cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
            //cspParams.ProviderName = _providerName;

            //_provider = new RSACryptoServiceProvider(cspParams);
        }

        #endregion

        #region Static Public Methods

        /// <summary>
        /// This function uses RSA Asymmetric encryption algorithm and returns the encrypted data.
        /// </summary>
        /// <param name="data">Input data that needs to be encrypted</param>
        /// <returns>Encrypted data using RSA Asymmetric encryption algorithm</returns>
        /// <example>
        ///     <code>
        /// 
        ///     String encryptedData = SecondIINoneMC.Core.CryptographyProvider.Encrypt("Encrypt me!");
        /// 
        ///     </code>
        /// </example>
        public static String Encrypt(String data)
        {

            Byte[] plainText;
            Byte[] cipherText;

            CspParameters cspParams;

            cspParams = new CspParameters(_rsaFullProvider);
            cspParams.KeyContainerName = _containerName;
            cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
            //cspParams.Flags = CspProviderFlags.NoFlags;
            cspParams.ProviderName = _providerName;

            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(cspParams);

            plainText = System.Text.Encoding.UTF8.GetBytes(data);

            // Set the provider's Key as public key.
            provider.FromXmlString(PublicKey);

            //read plaInt32ext, encrypt it to ciphertext

            // Check if we are able to encrpyt in one pass.
            Int32 blockSize = (provider.KeySize >> 3) - 11;

            if (plainText.Length <= blockSize)
            {
                cipherText = provider.Encrypt(plainText, false);
            }
            else
            {
                // The PlainText (data) will need to be broken into segments of 
                // size (ModulusSize=rsa.KeySize/8) - 11
                // Each of these segments will be encrypted separately 
                // and will return encrypted data equal to the ModulusSize 
                // (with at least 11 bytes of padding).
                Int32 modulusSize = blockSize + 11;

                using (MemoryStream inputStream = new MemoryStream(plainText))
                using (MemoryStream outputStream = new MemoryStream(blockSize))
                {
                    Byte[] buffer = new Byte[blockSize];
                    Int32 bytesRead;

                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, blockSize);
                        if (bytesRead == blockSize)
                        {
                            outputStream.Write(provider.Encrypt(buffer, false), 0, modulusSize);
                        }
                        else
                        {
                            Byte[] final = new Byte[bytesRead];
                            Array.Copy(buffer, final, bytesRead);
                            outputStream.Write(provider.Encrypt(final, false), 0, modulusSize);
                        }
                    } while (bytesRead == blockSize);

                    cipherText = outputStream.ToArray();
                }
            }

            return Convert.ToBase64String(cipherText);
        }

        /// <summary>
        /// This function decrypts the data encrypted using RSA Asymmetric encryption algorithm and returns the original data.
        /// </summary>
        /// <param name="data">Input data that needs to be decrypted</param>
        /// <returns>Decrypted data using RSA Asymmetric encryption algorithm</returns>
        /// <example>
        ///     <code>
        /// 
        ///         String decryptedData = SecondIINoneMC.Core.CryptographyProvider.Decrypt("dhg1k31n5Y+xyxGWFjZZkWdw20J7fPQ5mvVnqygjiMKNeGk/6K2flPsdtLKiWVlyLXZm/pCIIlMb0euT45yuguEtp/+Dd08HYY8QuihNgHsM0O1PNIzxATQwWp0O/ez8Z1j5YPGjmwJB6TbDotqDTYMzFTEVPal4XpK4qFRt4KlMjJDrnFUjd7809wCid9TrwGLEfL+IcbOlElTijmOjHm+xknmBQAYLUA7JiMTSNKA=");
        /// 
        ///     </code>
        /// </example>
        public static String Decrypt(String data)
        {
            Byte[] cipherText;
            Byte[] plainText;

            cipherText = Convert.FromBase64String(data);

            var cspParams = new CspParameters(_rsaFullProvider);
            cspParams.KeyContainerName = _containerName;
            cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
            //cspParams.Flags = CspProviderFlags.NoFlags;
            cspParams.ProviderName = _providerName;

            var provider = new RSACryptoServiceProvider(cspParams);

            provider.FromXmlString(PrivateKey);

            // Check if we are able to encrpyt in one pass.
            Int32 modulusSize = provider.KeySize >> 3;

            if (cipherText.Length == modulusSize)
            {
                plainText = provider.Decrypt(cipherText, false);
            }
            else
            {
                // The cipherText will need to be broken into segments 
                // of size rsa.KeySize/8
                // Each of these segments will be encrypted separately 
                // and will return encrypted data equal to the ModulusSize 
                // (with at least 11 bytes of padding).

                using (MemoryStream inputStream = new MemoryStream(cipherText))
                using (MemoryStream outputStream = new MemoryStream(modulusSize))
                {
                    Byte[] buffer = new Byte[modulusSize];
                    Int32 bytesRead;

                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, modulusSize);
                        if (bytesRead > 0)
                        {
                            Byte[] plain = provider.Decrypt(buffer, false);
                            outputStream.Write(plain, 0, plain.Length);
                            Array.Clear(plain, 0, plain.Length);
                        }
                    } while (bytesRead > 0);

                    plainText = outputStream.ToArray();
                }

            }

            if (plainText != null)
                return System.Text.Encoding.UTF8.GetString(plainText);
            else
                return null;

        }

        #endregion

        #endregion


    }
}
