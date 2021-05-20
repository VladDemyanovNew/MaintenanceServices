using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Utils
{
    public class Cryptographer
    {
        public static string GetMD5Hash(string text)
        {
            using (var hashAlg = MD5.Create())
            {
                byte[] hash = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(text));
                var builder = new StringBuilder(hash.Length * 2);
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("X2"));
                }
                return builder.ToString();
            }
        }
    }
}
