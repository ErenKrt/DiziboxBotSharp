using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EpEren.DiziBox.Helpers
{
    public static class Extra
    {
        public static string fnSeo(string parVeri)
        {
            if (string.IsNullOrEmpty(parVeri) == false)
            {
                parVeri = parVeri.Trim();
                parVeri = parVeri.Replace(".", "-");
                parVeri = parVeri.Replace("ã¢", "a");
                parVeri = parVeri.Replace("ã‚", "a");
                parVeri = parVeri.Replace("ãª", "e");
                parVeri = parVeri.Replace("ãš", "e");
                parVeri = parVeri.Replace("ã§", "c");
                parVeri = parVeri.Replace("ã‡", "c");
                parVeri = parVeri.Replace("äÿ", "g");
                parVeri = parVeri.Replace("ä", "g");
                parVeri = parVeri.Replace("ä°", "i");
                parVeri = parVeri.Replace("ä±", "i");
                parVeri = parVeri.Replace("ã¶", "o");
                parVeri = parVeri.Replace("ã–", "o");
                parVeri = parVeri.Replace("åÿ", "s");
                parVeri = parVeri.Replace("å", "s");
                parVeri = parVeri.Replace("ã¼", "u");
                parVeri = parVeri.Replace("ãœ", "u");
                parVeri = parVeri.Replace("â", "a");
                parVeri = parVeri.Replace("Â", "a");
                parVeri = parVeri.Replace("ê", "e");
                parVeri = parVeri.Replace("Ê", "e");
                parVeri = parVeri.Replace("ç", "c");
                parVeri = parVeri.Replace("Ç", "c");
                parVeri = parVeri.Replace("ğ", "g");
                parVeri = parVeri.Replace("Ğ", "g");
                parVeri = parVeri.Replace("İ", "i");
                parVeri = parVeri.Replace("I", "i");
                parVeri = parVeri.Replace("ı", "i");
                parVeri = parVeri.Replace("î", "i");
                parVeri = parVeri.Replace("Î", "i");
                parVeri = parVeri.Replace("î", "i");
                parVeri = parVeri.Replace("ö", "o");
                parVeri = parVeri.Replace("Ö", "o");
                parVeri = parVeri.Replace("ş", "s");
                parVeri = parVeri.Replace("Ş", "s");
                parVeri = parVeri.Replace("ü", "u");
                parVeri = parVeri.Replace("Ü", "u");
                parVeri = parVeri.Replace(" ", "-");
                parVeri = parVeri.ToLower();
                while (parVeri.IndexOf("--") > -1)
                {
                    parVeri = parVeri.Replace("--", "-");
                }
                parVeri = Regex.Replace(parVeri, @"[^a-z0-9\s-]", "");
            }
            return parVeri;
        }
    }
}
