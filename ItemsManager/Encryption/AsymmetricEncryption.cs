using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ItemsManager.Encryption
{
    internal class AsymmetricEncryption
    {
        public byte[] Encrypt(string stringToEncrypt, string certName)
        {
            return Encrypt(Encoding.Default.GetBytes(stringToEncrypt), certName);
        }

        public byte[] Encrypt(byte[] bytesToEncrypt, string certName)
        {
            X509Certificate2 cert = GetCert(certName) ?? throw new InvalidOperationException("Certificate not found.");
            using RSA? rsa = cert.GetRSAPublicKey(); // will be disposed

            // generate random AES key and nonce (nonce for GCM)
            byte[] aesKey = new byte[32]; // AES-256
            RandomNumberGenerator.Fill(aesKey);

            byte[] nonce = new byte[12]; // recommended 12 bytes for GCM
            RandomNumberGenerator.Fill(nonce);

            // encrypt AES key with RSA-OAEP-SHA256
            byte[] encryptedKey = rsa.Encrypt(aesKey, RSAEncryptionPadding.OaepSHA256);

            // encrypt data with AES-GCM
            byte[] ciphertext = new byte[bytesToEncrypt.Length];
            byte[] tag = new byte[16];

            using (var aesgcm = new AesGcm(aesKey))
            {
                aesgcm.Encrypt(nonce, bytesToEncrypt, ciphertext, tag);
            }

            // build output: keyLen(4) || ivLen(4) || encryptedKey || nonce || ciphertext || tag
            var keyLenBytes = BitConverter.GetBytes(encryptedKey.Length);
            var ivLenBytes = BitConverter.GetBytes(nonce.Length);

            using var ms = new MemoryStream();
            ms.Write(keyLenBytes, 0, 4);
            ms.Write(ivLenBytes, 0, 4);
            ms.Write(encryptedKey, 0, encryptedKey.Length);
            ms.Write(nonce, 0, nonce.Length);
            ms.Write(ciphertext, 0, ciphertext.Length);
            ms.Write(tag, 0, tag.Length);

            // clear sensitive key material
            Array.Clear(aesKey, 0, aesKey.Length);

            return ms.ToArray();
        }

        public byte[] Decrypt(byte[] bytesToDecrypt, string certName)
        {
            X509Certificate2 cert = GetCert(certName) ?? throw new InvalidOperationException("Certificate not found.");
            using RSA? rsa = cert.GetRSAPrivateKey();

            using var inStream = new MemoryStream(bytesToDecrypt);

            var keyLenBytes = new byte[4];
            var ivLenBytes = new byte[4];

            if (inStream.Read(keyLenBytes, 0, 4) != 4) throw new ArgumentException("Invalid data");
            if (inStream.Read(ivLenBytes, 0, 4) != 4) throw new ArgumentException("Invalid data");

            int keyLen = BitConverter.ToInt32(keyLenBytes, 0);
            int ivLen = BitConverter.ToInt32(ivLenBytes, 0);

            var encryptedKey = new byte[keyLen];
            if (inStream.Read(encryptedKey, 0, keyLen) != keyLen) throw new ArgumentException("Invalid data");

            var nonce = new byte[ivLen];
            if (inStream.Read(nonce, 0, ivLen) != ivLen) throw new ArgumentException("Invalid data");

            // remaining: ciphertext || tag (tag is 16 bytes for AES-GCM)
            int remaining = (int)(inStream.Length - inStream.Position);
            if (remaining < 16) throw new ArgumentException("Invalid data - missing tag/ciphertext");
            int ciphertextLen = remaining - 16;

            var ciphertext = new byte[ciphertextLen];
            if (inStream.Read(ciphertext, 0, ciphertextLen) != ciphertextLen) throw new ArgumentException("Invalid data");

            var tag = new byte[16];
            if (inStream.Read(tag, 0, 16) != 16) throw new ArgumentException("Invalid data");

            // decrypt AES key
            byte[] aesKey = rsa.Decrypt(encryptedKey, RSAEncryptionPadding.OaepSHA256);

            // decrypt with AES-GCM (will throw if authentication fails)
            var plaintext = new byte[ciphertextLen];
            using (var aesgcm = new AesGcm(aesKey))
            {
                aesgcm.Decrypt(nonce, ciphertext, tag, plaintext);
            }

            Array.Clear(aesKey, 0, aesKey.Length);

            return plaintext;
        }

        private X509Certificate2? GetCert(string certName)
        {
            using X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            // lepsze: porównywać Thumbprint lub użyć Find z X509FindType, zamiast porównywać SubjectName.Name
            return store.Certificates.Cast<X509Certificate2>().SingleOrDefault(x => x.SubjectName.Name == certName);
        }
    }

}
