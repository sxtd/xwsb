using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace xwsb0._11
{
    /// <summary>
    /// 加密类
    /// </summary>
    class CryptClass
    {
        //密钥向量
        private static byte[] Keys={0x12,0x34,0x56,0x78,0x90,0xAB,0xCD,0xEF};
        public static string EncryptKey = "12345678";

        /// <summary>
        /// DES方式加密字符串
        /// </summary>
        /// <param name="encryptString">encryptString要加密的字符串</param>
        /// <param name="encryptKey">encryptKey加密密钥，要求8位</param>
        /// <returns>返回加密成功后的字符串，失败返回原串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);

                DESCryptoServiceProvider dcsp = new DESCryptoServiceProvider();

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dcsp.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);

                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥，要求8位，要求和加密密钥相同</param>
        /// <returns>解密成功后，返回解密后字符串，失败返回源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            if(decryptString == "")
            {
                return "";
            }
            else
            {
                try
                {
                    byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                    byte[] rgbIV = Keys;
                    byte[] inputByteArray = Convert.FromBase64String(decryptString);

                    DESCryptoServiceProvider dcsp = new DESCryptoServiceProvider();
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, dcsp.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);

                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
                catch
                {
                    return "";
                }
            }
        }
    }
}
