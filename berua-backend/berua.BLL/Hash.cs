using System.Security.Cryptography;
using System.Text;

namespace berua.BLL
{
    public class Hash
    {
        public static byte[] CreateSalt(int size)
        {
            //Генерация рандомного криптографического ключа
            using (var rngCrypto = new RNGCryptoServiceProvider())
            {
                byte[] buff = new byte[size];
                rngCrypto.GetBytes(buff);
                //Возврат строкового представления случайного числа Base64.
                return buff;
            }
        }

        public static byte[] GenerateSaltedHash(string password, byte[] salt)
        {
            using (HashAlgorithm algorithm = new SHA256Managed())
            {
                var plainText = Encoding.UTF8.GetBytes(password);

                byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

                for (int i = 0; i < plainText.Length; i++)
                    plainTextWithSaltBytes[i] = plainText[i];

                for (int i = plainText.Length; i < salt.Length; i++)
                    plainTextWithSaltBytes[i] = salt[i];

                return algorithm.ComputeHash(plainTextWithSaltBytes);
            }
        }

        public static bool CompareByteArrays(byte[] arr1, byte[] arr2)
        {
            if (arr1.Length != arr2.Length)
                return false;

            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i])
                    return false;
            }
            return true;
        }
    }
}

