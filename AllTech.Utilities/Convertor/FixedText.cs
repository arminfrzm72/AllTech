using System;
using System.Collections.Generic;
using System.Text;

namespace AllTech.Utilities.Convertor
{
    public class FixedText
    {
        public static string FixedEmail(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
