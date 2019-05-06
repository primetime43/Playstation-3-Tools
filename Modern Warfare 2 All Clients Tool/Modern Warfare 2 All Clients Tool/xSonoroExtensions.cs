using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Warfare_2_All_Clients_Tool
{
    public static class xSonoroExtensions
    {
        public static string Fix(this System.Windows.Forms.NumericUpDown x)
        {
            return ((uint) x.Value).ToString("X2")+ "000";
        }

        public static string Fix(this int x)
        {
            return ((uint)x).ToString("X2") + "000";
        }

       public static string FixEndianess(this System.Windows.Forms.NumericUpDown x)
       {
           return BitConverter.ToString(BitConverter.GetBytes(Convert.ToInt32(x.Value))).Replace("-", "");
       }
        public static byte[] Reverse(byte[] inarray)
        {
           //Array.Reverse(inarray);
            return inarray;
        }
        public static string CalculateTimePlayed(int days, int hours, int minutes)
        {
            return BitConverter.ToString(Reverse(BitConverter.GetBytes((days*24*60*60) + (hours*60*60) + minutes*60))).Replace("-","");
        }
        public static string ToEndian(this int x, bool reverse)
        {
            byte[] a = BitConverter.GetBytes(x);
            if (reverse)
            {
                Array.Reverse(a);
            }
            return BitConverter.ToString(a).Replace("-", "");
        }
    }
}
