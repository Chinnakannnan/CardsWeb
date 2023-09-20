using PPICards.Models;
using System.Security.Cryptography;
using System.Text;

namespace PPICards.Helper
{
    public static class Security
    {
        private static string EncryptionKey = DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(" ", "secure");

        public static string EncryptionKeyOTP = OnboardConstants.EMAILKEY;
        public static string encrypt(this string encryptString)
        {
            if (string.IsNullOrEmpty(encryptString)) { return encryptString; }
            //string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }     
        public static string Decrypt(this string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return cipherText;
            }
            //string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static string AES_ENCRYPT(string clearText)
        {
            try
            {
                byte[] clearBytes = Encoding.ASCII.GetBytes(clearText);
                byte[] bENVVALUE;
                using (Aes encryptor = Aes.Create())
                {
                    byte[] bKey = Encoding.ASCII.GetBytes(EncryptionKeyOTP);// util.HexStringToByte(EncryptionKey);//Encoding.ASCII.GetBytes(EncryptionKey);//

                    encryptor.Key = bKey;
                    encryptor.Mode = CipherMode.ECB;
                    encryptor.Padding = PaddingMode.PKCS7;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        bENVVALUE = ms.ToArray();
                        clearText = Convert.ToBase64String(bENVVALUE);//util.ByteArrayToHexString(bENVVALUE);   //Convert.ToBase64String(bENVVALUE);
                    }
                }
            }
            catch (Exception)
            {

                return "";
            }
            finally
            {

            }
            return clearText;
        }
        public static string AES_DECRYPT(string cipherText)
        {
            try
            {
                byte[] bDECVALUE;
                byte[] cipherBytes = Convert.FromBase64String(cipherText); //util.HexStringToByte(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    byte[] bKey = Encoding.ASCII.GetBytes(EncryptionKeyOTP);// util.HexStringToByte(EncryptionKey);  //Encoding.ASCII.GetBytes(EncryptionKey);// 
                    encryptor.Key = bKey;
                    encryptor.Mode = CipherMode.ECB;
                    encryptor.Padding = PaddingMode.PKCS7;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        bDECVALUE = ms.ToArray();
                        cipherText = Encoding.ASCII.GetString(ms.ToArray());
                    }
                }
                return cipherText;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}
