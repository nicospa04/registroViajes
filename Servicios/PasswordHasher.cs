using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    // Método para generar elHashPassword hash de una contraseña con un salt

    public   byte[] GetAesKey(string key, int length = 32)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        Array.Resize(ref keyBytes, length); // Ajustar la longitud de la clave
        return keyBytes;
    }

    public string HashPassword(string plainText, string key)
    {
        byte[] encrypted;

        using (Aes aes = Aes.Create())
        {
            // Ajustar la clave a 32 bytes para AES-256
            aes.Key = GetAesKey(key, 32);
            aes.IV = aes.Key.Take(16).ToArray(); // Usamos los primeros 16 bytes de la clave como IV (recomendable usar un IV aleatorio en producción)

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    encrypted = ms.ToArray();
                }
            }
        }
        return Convert.ToBase64String(encrypted);
    }


    // Método para verificar la contraseña
    public   string Decrypt(string cipherText, string key)
    {
        // Ajustar la clave a 32 bytes para AES-256
        byte[] keyBytes = GetAesKey(key, 32);
        string decrypted;

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = keyBytes.Take(16).ToArray(); // Usamos los primeros 16 bytes como IV

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        decrypted = sr.ReadToEnd();
                    }
                }
            }
        }
        return decrypted;
    }
}
