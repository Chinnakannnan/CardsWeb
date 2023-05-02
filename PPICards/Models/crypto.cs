using System.Security.Cryptography;
using System.Text;

public static class crypto
{

    public static string AES_ENCRYPT(string clearText, string EncryptionKey)
    {
        try
        {
            byte[] clearBytes = Encoding.ASCII.GetBytes(clearText);
            byte[] bENVVALUE;
            using (Aes encryptor = Aes.Create())
            {
                byte[] bKey = Encoding.ASCII.GetBytes(EncryptionKey);// util.HexStringToByte(EncryptionKey);//Encoding.ASCII.GetBytes(EncryptionKey);//

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

    public static string AES_DECRYPT(string cipherText, string EncryptionKey)
    {
        try
        {
            byte[] bDECVALUE;
            byte[] cipherBytes = Convert.FromBase64String(cipherText); //util.HexStringToByte(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                byte[] bKey = Encoding.ASCII.GetBytes(EncryptionKey);// util.HexStringToByte(EncryptionKey);  //Encoding.ASCII.GetBytes(EncryptionKey);// 
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
        catch (Exception)
        {
            return "";
        }
    }

    public static string Decrypt_Ista_CBC(string combinedString, string keyString)
    {
        string plainText;
        byte[] combinedData = Convert.FromBase64String(combinedString);
        Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(keyString);
        byte[] iv = new byte[aes.BlockSize / 8];
        byte[] cipherText = new byte[combinedData.Length - iv.Length];
        Array.Copy(combinedData, iv, iv.Length);
        Array.Copy(combinedData, iv.Length, cipherText, 0, cipherText.Length);
        aes.IV = iv;
        aes.Mode = CipherMode.CBC;
        ICryptoTransform decipher = aes.CreateDecryptor(aes.Key, aes.IV);

        using (MemoryStream ms = new MemoryStream(cipherText))
        {
            using (CryptoStream cs = new CryptoStream(ms, decipher, CryptoStreamMode.Read))
            {
                using (StreamReader sr = new StreamReader(cs))
                {
                    plainText = sr.ReadToEnd();
                }
            }

            return plainText;
        }
    }
    public static string Encrypt_Ista_CBC(string plainText, string keyString)
    {
        byte[] cipherData;
        Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(keyString);
        aes.GenerateIV();
        aes.Mode = CipherMode.CBC;
        ICryptoTransform cipher = aes.CreateEncryptor(aes.Key, aes.IV);

        using (MemoryStream ms = new MemoryStream())
        {
            using (CryptoStream cs = new CryptoStream(ms, cipher, CryptoStreamMode.Write))
            {
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }
            }

            cipherData = ms.ToArray();
        }

        byte[] combinedData = new byte[aes.IV.Length + cipherData.Length];
        Array.Copy(aes.IV, 0, combinedData, 0, aes.IV.Length);
        Array.Copy(cipherData, 0, combinedData, aes.IV.Length, cipherData.Length);
        return Convert.ToBase64String(combinedData);
    }

    public static string AES_ENCRYPT_CBC(string clearText, string EncryptionKey, string IV)
    {
        try
        {
            byte[] clearBytes = Encoding.ASCII.GetBytes(clearText);
            byte[] bENVVALUE;
            using (Aes encryptor = Aes.Create())
            {
                byte[] bKey = ASCIIEncoding.UTF8.GetBytes(EncryptionKey);//Encoding.ASCII.GetBytes(EncryptionKey);//
                byte[] bIV = ASCIIEncoding.UTF8.GetBytes(IV);

                encryptor.Key = bKey;
                encryptor.IV = bIV;
                encryptor.Mode = CipherMode.CBC;
                encryptor.Padding = PaddingMode.PKCS7;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    bENVVALUE = ms.ToArray();
                    string d = BitConverter.ToString(bENVVALUE);
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

    public static string AES_DECRYPT_CBC(string cipherText, string EncryptionKey)
    {
        try
        {
            byte[] bDECVALUE;
            byte[] cipherBytes = Convert.FromBase64String(cipherText); //util.HexStringToByte(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                byte[] bKey = ASCIIEncoding.UTF8.GetBytes(EncryptionKey);  //Encoding.ASCII.GetBytes(EncryptionKey);// 
                byte[] bIV = new byte[16];

                Array.Copy(cipherBytes, bIV, 16);

                byte[] bData = new byte[cipherBytes.Length - 16];

                Array.Copy(cipherBytes, 16, bData, 0, cipherBytes.Length - 16);

                encryptor.Key = bKey;
                encryptor.IV = bIV;
                encryptor.Mode = CipherMode.CBC;
                encryptor.Padding = PaddingMode.PKCS7;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bData, 0, bData.Length);
                        cs.Close();
                    }
                    bDECVALUE = ms.ToArray();
                    string d = BitConverter.ToString(bDECVALUE);
                    cipherText = Encoding.ASCII.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        catch (Exception)
        {
            return "";
        }
    }

    internal static string GetHMACSHA256(object p, object mERCHANTSECRET)
    {
        throw new NotImplementedException();
    }

    private static string getSalt()
    {
        byte[] bytes = new byte[128 / 8];
        using (var keyGenerator = RandomNumberGenerator.Create())
        {
            keyGenerator.GetBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }

    public static string GETHASH(string text)
    {
        // SHA512 is disposable by inheritance.  
        using (var sha512 = SHA512.Create())
        {
            // Send a sample text to hash.  
            var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(text));
            // Get the hashed string.  
            //return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return Convert.ToBase64String(hashedBytes);
        }
    }

    public static string GetHMACSHA512(string text, string key)
    {
        ASCIIEncoding encoder = new ASCIIEncoding();
        //  Log obj = new Log();

        //obj.WriteAppLog(text);
        //obj.WriteAppLog(key);

        byte[] hashValue;
        byte[] keybyt = ASCIIEncoding.ASCII.GetBytes(key);
        byte[] message = ASCIIEncoding.ASCII.GetBytes(text);

        HMACSHA512 hashString = new HMACSHA512(keybyt);
        string hex = "";

        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }
}