using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AllTech.Utilities.Security
{
    public static class PasswordHelper
    {

        public static string EncodePasswordMd5(string pass)//encrypt using MD5
        {
            byte[] orginalBytes;
            byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider,get bytes for orginal password
            md5 = new MD5CryptoServiceProvider();
            orginalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(orginalBytes);
            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
            
        }
    }
}
