using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwncmsCore.Services.Encript
{
    public class Encript
    {

        //Get Encript Phone Start
        public static string Get_Enc(string str)
        {
            byte[] text = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
            string result = Convert.ToBase64String(text);
            return result;
        }
        //Get Encript Phone End



        //Get Encript Phone Start
        public static string Get_Dec(string str)
        {
            byte[] text = Convert.FromBase64String(str);
            string result = System.Text.ASCIIEncoding.ASCII.GetString(text);
            return result;
        }
        //Get Encript Phone End

    }
}