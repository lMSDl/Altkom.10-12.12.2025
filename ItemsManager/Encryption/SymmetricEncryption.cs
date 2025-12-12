using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ItemsManager.Encryption
{
    internal class SymmetricEncryption
    {
        private Aes _algorithm = Aes.Create();

        private const int SaltSize = 16;

        public byte[] Encrypt(string stringToEncrypt, string password)
        {
            return Encrypt(Encoding.Default.GetBytes(stringToEncrypt), password);
        }

        public byte[] Encrypt(byte[] bytesToEncrypt, string password)
        {
            // generuj losową sól
            byte[] salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            // derive key z tej soli
            using var passwordHash = GenerateHash(password, salt);
            var key = GenerateKey(passwordHash);

            // ustaw klucz i wygeneruj losowe IV
            _algorithm.Key = key;
            _algorithm.GenerateIV();
            var iv = _algorithm.IV;

            var encryptor = _algorithm.CreateEncryptor(key, iv);
            var ciphertext = Transform(bytesToEncrypt, encryptor);

            // wynik: salt || iv || ciphertext
            var result = new byte[salt.Length + iv.Length + ciphertext.Length];
            int offset = 0;
            Buffer.BlockCopy(salt, 0, result, offset, salt.Length);
            offset += salt.Length;
            Buffer.BlockCopy(iv, 0, result, offset, iv.Length);
            offset += iv.Length;
            Buffer.BlockCopy(ciphertext, 0, result, offset, ciphertext.Length);

            // wyczyść klucz z pamięci (opcjonalnie)
            Array.Clear(key, 0, key.Length);

            return result;
        }

        public string Decrypt(byte[] bytesToDecrypt, string password)
        {
            if (bytesToDecrypt == null) throw new ArgumentNullException(nameof(bytesToDecrypt));
            int ivLength = _algorithm.BlockSize / 8;
            int minimumLength = SaltSize + ivLength + 1;
            if (bytesToDecrypt.Length < minimumLength) throw new ArgumentException("Invalid encrypted data (too short).");

            // odczytaj sól
            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(bytesToDecrypt, 0, salt, 0, SaltSize);

            // odczytaj IV
            byte[] iv = new byte[ivLength];
            Buffer.BlockCopy(bytesToDecrypt, SaltSize, iv, 0, ivLength);

            // odszyfruj resztę jako ciphertext
            int ciphertextOffset = SaltSize + ivLength;
            int ciphertextLength = bytesToDecrypt.Length - ciphertextOffset;
            byte[] ciphertext = new byte[ciphertextLength];
            Buffer.BlockCopy(bytesToDecrypt, ciphertextOffset, ciphertext, 0, ciphertextLength);

            // derive key i utwórz decryptor
            using var passwordHash = GenerateHash(password, salt);
            var key = GenerateKey(passwordHash);

            var decryptor = _algorithm.CreateDecryptor(key, iv);
            var bytes = Transform(ciphertext, decryptor);

            // wyczyść klucz z pamięci (opcjonalnie)
            Array.Clear(key, 0, key.Length);

            return Encoding.Default.GetString(bytes);
        }

        private static byte[] Transform(byte[] bytes, ICryptoTransform cryptoTransform)
        {
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);

            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();

            return memoryStream.ToArray();
        }

        private byte[] GenerateKey(Rfc2898DeriveBytes passwordHash)
        {
            return passwordHash.GetBytes(_algorithm.KeySize / 8);
        }

        private Rfc2898DeriveBytes GenerateHash(string password, byte[] salt)
        {
            // warto dodać iteracje/HashAlgorithmName dla bezpieczeństwa
            return new Rfc2898DeriveBytes(password, salt);
        }
    }
}
