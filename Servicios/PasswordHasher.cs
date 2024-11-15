using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    // Método para generar elHashPassword hash de una contraseña con un salt
    
    public   string HashPassword(string plainText, string key)
    {
        byte[] encrypted;

        // Crear una instancia de AES con la clave y un IV
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = aes.Key.Take(16).ToArray(); // Usamos los primeros 16 bytes de la clave como IV (recomendable usar un IV diferente en producción)

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
        string decrypted;

        // Crear una instancia de AES con la clave y el mismo IV utilizado para encriptar
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = aes.Key.Take(16).ToArray();

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
