using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

//https://codeday.me/es/qa/20181214/6178.html
/*Advanced Encryption Standard (AES), también conocido como Rijndael (pronunciado "Rain Doll" en inglés), 
 * es un esquema de cifrado por bloques adoptado como un estándar de cifrado por el gobierno de los Estados 
 * Unidos. El AES fue anunciado por el Instituto Nacional de Estándares y Tecnología (NIST) como FIPS PUB 197
 * de los Estados Unidos (FIPS 197) el 26 de noviembre de 2001 después de un proceso de estandarización que 
 * duró 5 años. Se transformó en un estándar efectivo el 26 de mayo de 2002. Desde 2006, el AES es uno de los
 * algoritmos más populares usados en criptografía simétrica.
El cifrado fue desarrollado por dos criptólogos belgas, Joan Daemen y Vincent Rijmen, ambos estudiantes de la
Katholieke Universiteit Leuven, y fue enviado al proceso de selección AES bajo el nombre "Rijndael".*/

namespace EncryptStringSample
{
    public static class StringCipher
    {
        // Esta constante se utiliza para determinar el tamaño de clave del algoritmo de cifrado en bits.
        // Dividimos esto por 8 dentro del código a continuación para obtener el número equivalente de bytes.
        private const int Keysize = 256;

        // Esta constante determina el número de iteraciones para la función de generación de bytes de contraseña.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            // Salt y IV se genera aleatoriamente cada vez, pero se agrega al texto cifrado cifrado
            // para que se puedan usar los mismos valores de sal y IV al descifrar. 
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Cree los bytes finales como una concatenación de los bytes de sal aleatorios, los bytes iv aleatorios y los bytes de cifrado.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            // Obtenga la secuencia completa de bytes que representan:
            // [32 bytes de Salt] + [32 bytes de IV] + [n bytes de CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Obtenga los saltbytes extrayendo los primeros 32 bytes de los bytes de texto cifrado suministrados.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Obtenga los bytes IV extrayendo los siguientes 32 bytes de los bytes de texto cifrado suministrados.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Obtenga los bytes de texto de cifrado reales eliminando los primeros 64 bytes de la cadena de texto de cifrado.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 bytes nos darán 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Rellene la matriz con bytes aleatorios criptográficamente seguros.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
