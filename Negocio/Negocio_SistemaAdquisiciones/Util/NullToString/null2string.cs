using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeAdquisiciones.Util.NullToString
{
   public class null2string
    {
        public static string NullToString(object Value)
        {

            // Value.ToString() allows for Value being DBNull, but will also convert int, double, etc.
            return Value == null ? "" : Value.ToString();

            

        }
    }
}
