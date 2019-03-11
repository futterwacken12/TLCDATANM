using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Helpers
{
    public class AppHelpers
    {
        public static string GenerarToken()
        {
            long Fecha = DateTime.UtcNow.ToBinary();
            byte[] time = BitConverter.GetBytes(Fecha);
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }
        public static bool ValidarToken(string token)
        {
            byte[] data = Convert.FromBase64String(token);
            long dataInt = BitConverter.ToInt64(data, 0);
            DateTime when = DateTime.FromBinary(dataInt);
            if (when < DateTime.UtcNow.AddHours(-24))
                return false;
            return true;
        }
        public static string Base64_Decode(string str)
        {
            try
            {
                byte[] decbuff = Convert.FromBase64String(str);
                return Encoding.UTF8.GetString(decbuff);
            }
            catch
            {
                return "";
            }
        }
        public static string Base64_Encode(string str)
        {
            try
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(str);
                return Convert.ToBase64String(plainTextBytes);
            }
            catch
            {
                return "";
            }
        }
    }
}