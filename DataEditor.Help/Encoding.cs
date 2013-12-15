using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class Encoding
    {
        public static string Turnto(string origin, System.Text.Encoding from, System.Text.Encoding to)
        {
            return to.GetString(to.GetBytes(origin));
        }
    }
}
