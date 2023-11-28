using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;


namespace CipherText {
    internal class Program {
        // use this method to gen keyparam
        // https://www.bouncycastle.org/
        public static ICipherParameters keyParameterGenerationMethod1(int keySize) {
            SecureRandom randvalue = new SecureRandom();
            CipherKeyGenerator keyGen = new CipherKeyGenerator();
            keyGen.Init(new KeyGenerationParameters(randvalue, 256));
            KeyParameter keyParam = keyGen.GenerateKeyParameter();
            return keyParam;
        }

        // use this method to gen keyparam
        // https://www.bouncycastle.org/
        public static ICipherParameters keyParameterGenerationMethod2(byte[] myKey) {
            ICipherParameters keyParam = new KeyParameter(myKey);
            return keyParam;
        }

        // https://www.bouncycastle.org/
        public static byte[] ecbPaddedEncrypt(ICipherParameters keyParam, byte[] plainTextData) {
            // First choose the "engine", in this case AES                           
            IBlockCipher symmetricBlockCipher = new AesEngine();
            // Next select the mode compatible with the "engine", in this case we use the simple ECB mode
            IBlockCipherMode symmetricBlockMode = new EcbBlockCipher(symmetricBlockCipher);
            // Finally select a compatible padding, PKCS7 which is the default
            IBlockCipherPadding padding = new Pkcs7Padding();
            PaddedBufferedBlockCipher ecbCipher = new PaddedBufferedBlockCipher(symmetricBlockMode, padding);
            // apply the mode and engine on the plainTextData
            ecbCipher.Init(true, keyParam);
            int blockSize = ecbCipher.GetBlockSize();
            byte[] cipherTextData = new byte[ecbCipher.GetOutputSize(plainTextData.Length)];
            int processLength = ecbCipher.ProcessBytes(plainTextData, 0, plainTextData.Length, cipherTextData, 0);
            int finalLength = ecbCipher.DoFinal(cipherTextData, processLength);
            byte[] finalCipherTextData = new byte[cipherTextData.Length - (blockSize - finalLength)];
            Array.Copy(cipherTextData, 0, finalCipherTextData, 0, finalCipherTextData.Length);
            return finalCipherTextData;
        }

        // https://www.bouncycastle.org/
        public static byte[] ecbPaddedDecrypt(ICipherParameters keyParam, byte[] cipherTextData) {
            // First choose the "engine", in this case AES
            IBlockCipher symmetricBlockCipher = new AesEngine();
            // Next select the mode compatible with the "engine", in this case we use the simple ECB mode
            IBlockCipherMode symmetricBlockMode = new EcbBlockCipher(symmetricBlockCipher);
            // Finally select a compatible padding, PKCS7 which is the default
            IBlockCipherPadding padding = new Pkcs7Padding();
            PaddedBufferedBlockCipher ecbCipher =
            new PaddedBufferedBlockCipher(symmetricBlockMode, padding);
            // apply the mode and engine on the plainTextData
            ecbCipher.Init(false, keyParam);
            int blockSize = ecbCipher.GetBlockSize();
            byte[] plainTextData = new byte[ecbCipher.GetOutputSize(cipherTextData.Length)];
            int processLength =
            ecbCipher.ProcessBytes(cipherTextData, 0, cipherTextData.Length, plainTextData, 0);
            int finalLength = ecbCipher.DoFinal(plainTextData, processLength);
            byte[] finalPlainTextData = new byte[plainTextData.Length - (blockSize - finalLength)];
            Array.Copy(plainTextData, 0, finalPlainTextData, 0, finalPlainTextData.Length);
            return finalPlainTextData;
        }

        public static void Main(string[] args) {
            string ans = "";
            string? mess = "";
            ICipherParameters abc = keyParameterGenerationMethod1(256);
            // read line
            Console.Write("Enter in a Plain text to encrypt >> ");
            mess = Console.ReadLine() ?? "";
            // encrypt
            byte[] ct = ecbPaddedEncrypt(abc, Encoding.ASCII.GetBytes(mess));
            string tmp = Encoding.ASCII.GetString(ct);
            // printout length of cipher-text and ciphertext
            Console.WriteLine($"Cipher text: {tmp} Length: {tmp.Length.ToString()}" );
            // decrypt
            byte[] pt = ecbPaddedDecrypt(abc, ct);
            ans = Encoding.UTF8.GetString(pt);
            Console.WriteLine(ans);

        }
    }
}
