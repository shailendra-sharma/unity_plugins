using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public sealed class Util
{
    public static byte[] EncryptData(string normalText, string key, string iv, PaddingMode paddingMode = PaddingMode.PKCS7)
    {
        try
        {
            using (AesManaged aesAlgo = new AesManaged())
            {
                aesAlgo.Key = Convert.FromBase64String(key);
                aesAlgo.IV = Convert.FromBase64String(iv);
                aesAlgo.Padding = paddingMode;
                aesAlgo.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = aesAlgo.CreateEncryptor(aesAlgo.Key, aesAlgo.IV);
                return WriteEncryptedData(normalText, encryptor);
            }
        }
        catch (Exception e)
        {
            Debug.LogFormat("EncryptData->>Exception:{0}", e.Message);
        }
        return null;
    }

    private static byte[] WriteEncryptedData(string plainText, ICryptoTransform encryptor)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter writer = new StreamWriter(cryptoStream))
                {
                    writer.Write(plainText);
                }
            }

            return memoryStream.ToArray();
        }
    }

    public static string DecryptBytes(byte[] encryptedBytes, string key, string iv, PaddingMode paddingMode = PaddingMode.PKCS7)
    {
        try
        {
            using (AesManaged aesAlgo = new AesManaged())
            {
                aesAlgo.Key = Convert.FromBase64String(key);
                aesAlgo.IV = Convert.FromBase64String(iv);
                aesAlgo.Mode = CipherMode.CBC;
                aesAlgo.Padding = paddingMode;

                ICryptoTransform decryptor = aesAlgo.CreateDecryptor(aesAlgo.Key, aesAlgo.IV);

                return ReadCipherBytesToString(encryptedBytes, decryptor);
            }
        }
        catch (Exception e)
        {
            Debug.LogFormat("DecryptBytes->>Exception:{0}", e.Message);
        }
        return null;
    }

    private static string ReadCipherBytesToString(byte[] cipherBytes, ICryptoTransform decryptor)
    {
        if (cipherBytes != null)
        {
            using (MemoryStream memoryStream = new MemoryStream(cipherBytes))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }

        return null;
    }

    public static byte[] ReadAllBytesFromStream(Stream inputStream)
    {
        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = inputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }

    public static byte[] CompressToBytes(byte[] data)
    {
        if (data != null)
        {
            try
            {
                return Ionic.Zlib.ZlibStream.CompressBuffer(data);
            }
            catch (Exception e)
            {
                Debug.LogFormat("CompressToBytes->>Exception:{0}", e.Message);
            }
        }
        return null;
    }

    public static byte[] DecompressBytes(byte[] compressedBytes)
    {
        if (compressedBytes != null)
        {
            try
            {
                return Ionic.Zlib.ZlibStream.UncompressBuffer(compressedBytes);
            }
            catch (Exception e)
            {
                Debug.LogFormat("DecompressBytes->>Exception:{0}", e.Message);
            }
        }

        return null;
    }


    public static bool WriteFile(string filePath, byte[] bytes)
    {
        if (bytes != null)
        {
            try
            {
                CreateDirectory(filePath);
                File.WriteAllBytes(filePath, bytes);

                Debug.LogFormat("WriteFile->>Write file to {0}", filePath);

                return true;
            }
            catch (Exception e)
            {
                Debug.LogFormat("WriteFile->>Exception:{0}", e.Message);
            }

            Debug.LogFormat("WriteFile->>Unable to write file to {0}", filePath);
        }
        return false;
    }

    public static void CreateDirectory(string filePath)
    {
        try
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                string dirName = new FileInfo(filePath).DirectoryName;
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogFormat("CreateDirectory->>Exception:{0}", e.Message);
        }
    }

    public static byte[] ReadFile_Bytes(string filePath, string logPrefix = "RG_Util->>ReadFile_Bytes->>")
    {
        if (File.Exists(filePath))
        {
            try
            {
                return File.ReadAllBytes(filePath);
            }
            catch (Exception e)
            {
                Debug.LogFormat("CreateDirectory->>Exception:{0}", e.Message);
            }
        }
        return null;
    }
}
