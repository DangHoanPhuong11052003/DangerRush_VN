using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class EncryptionData
{
    private static readonly string key = "1234567890abcdef1234567890abcdef";
    private static readonly string iv = "abcdef1234567890";

    public static string EncryptAES(string data)
    {
        byte[] keyBytes=Encoding.UTF8.GetBytes(key);
        byte[] ivBytes=Encoding.UTF8.GetBytes(iv);
        byte[] dataBytes=Encoding.UTF8.GetBytes(data);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = ivBytes;

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (MemoryStream ms = new MemoryStream())
            {
                using(CryptoStream cs=new CryptoStream(ms,aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (StreamWriter sr = new StreamWriter(cs))
                    {
                        sr.Write(data);
                    }
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public static string DeEncryptAES(string data)
    {
        byte[] keyBytes= Encoding.UTF8.GetBytes(key);
        byte[] ivBytes= Encoding.UTF8.GetBytes(iv);
        byte[] dataBytes= Convert.FromBase64String(data);

        using (Aes aes = Aes.Create())
        {
            aes.Key= keyBytes;
            aes.IV= ivBytes;

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (MemoryStream ms= new MemoryStream(dataBytes))
            {
                using(CryptoStream cs=new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader sr=new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }

    public static string EncryptSHA256(string data)
    {
        using(SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes= Encoding.UTF8.GetBytes(data);
            byte[] hash= sha256.ComputeHash(bytes);

            StringBuilder sb= new StringBuilder();
            foreach (var item in hash)
            {
                sb.Append(item.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
