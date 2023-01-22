using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Services
{
    public static class CharReplace
    {
        public static string TurkishCharReplace(this string str)
        {
            string once, sonra;
            once = str;
            sonra = once.Replace('ı', 'i');
            once = sonra.Replace('ö', 'o');
            sonra = once.Replace('ü', 'u');
            once = sonra.Replace('ş', 's');
            sonra = once.Replace('ğ', 'g');
            once = sonra.Replace('ç', 'c');
            sonra = once.Replace('İ', 'I');
            once = sonra.Replace('Ö', 'O');
            sonra = once.Replace('Ü', 'U');
            once = sonra.Replace('Ş', 'S');
            sonra = once.Replace('Ğ', 'G');
            once = sonra.Replace('Ç', 'C');
            str = once;
            return str;

        }
    }
}
