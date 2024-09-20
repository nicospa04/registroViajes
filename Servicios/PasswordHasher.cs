using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    // Método para generar el hash de una contraseña con un salt
    public string HashPassword(string password, out string salt)
    {
        // Generar un salt aleatorio
        byte[] saltBytes = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        salt = Convert.ToBase64String(saltBytes);

        // Combinar el salt y la contraseña antes de hashear
        string saltedPassword = salt + password;

        // Crear el hash usando SHA-256
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    // Método para verificar la contraseña
    public bool VerifyPassword(string password, string salt, string hashedPassword)
    {
        // Combinar el salt y la contraseña ingresada
        string saltedPassword = salt + password;

        // Crear el hash de la contraseña ingresada con el salt recuperado
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            string hashedInputPassword = Convert.ToBase64String(hashedBytes);

            // Comparar el hash generado con el hash almacenado
            return hashedInputPassword == hashedPassword;
        }
    }
}
