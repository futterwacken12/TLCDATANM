using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CHS.TLC.Data.NM.Web.Helpers
{

    public static class ConvertHelpers
    {
        private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public static DateTime FirstDayOfWeek(this DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return dt.AddDays(-diff).Date;
        }

        public static DateTime LastDayOfWeek(this DateTime dt)
        {
            return dt.FirstDayOfWeek().AddDays(6);
        }

        public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfNextMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1);
        }
        private static bool IsSimpleType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsSimpleType(type.GetGenericArguments()[0]);
            }
            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(decimal))
              || type.Equals(typeof(TimeSpan))
              || type.Equals(typeof(DateTime));
        }
        
        public static DataTable ToListDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                if (IsSimpleType(prop.PropertyType))
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    if (IsSimpleType(prop.PropertyType))
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable ToDataTable<T>(T data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                if (IsSimpleType(prop.PropertyType))
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }


            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                if (IsSimpleType(prop.PropertyType))
                    row[prop.Name] = prop.GetValue(data) ?? DBNull.Value;
            table.Rows.Add(row);

            return table;
        }
        public static string MD5HEX(this String input)
        {

            byte[] ByteData = Encoding.ASCII.GetBytes(input);
            MD5 oMd5 = MD5.Create();
            byte[] bytes = oMd5.ComputeHash(ByteData);


            char[] c = new char[bytes.Length * 2];

            byte b;

            for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
            {
                b = ((byte)(bytes[bx] >> 4));
                c[cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);

                b = ((byte)(bytes[bx] & 0x0F));
                c[++cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);
            }

            return new string(c);
        }
        public static String TostringUTF(this String val)
        {
            return Encoding.UTF8.GetString(Encoding.GetEncoding(1252).GetBytes(val ?? String.Empty));
        }
        public static string ToMD5(this String input)
        {
            byte[] ByteData = Encoding.ASCII.GetBytes(input);
            //MD5 creating MD5 object.
            MD5 oMd5 = MD5.Create();
            //Hash değerini hesaplayalım.
            byte[] HashData = oMd5.ComputeHash(ByteData);

            //convert byte array to hex format
            StringBuilder oSb = new StringBuilder();

            for (int x = 0; x < HashData.Length; x++)
            {
                //hexadecimal string value
                oSb.Append(HashData[x].ToString("x2"));
            }
            return oSb.ToString();
        }
        public static string ToHexString(this string str)
        {
            var sb = new StringBuilder();

            var bytes = Encoding.Unicode.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        public static string FromHexString(this string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.Unicode.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
        }
        public static String TostringUTF8(this String val)
        {
            return val.Replace("á", "&aacute;").Replace("é", "&eacute;").Replace("í", "&iacute;").Replace("ó", "&oacute;").Replace("ú", "&uacute;")
                      .Replace("Á", "&Aacute;").Replace("É", "&Eacute;").Replace("Í", "&Iacute;").Replace("Ó", "&Oacute;").Replace("Ú", "&Uacute;");
        }
        public static String ToStringJavaScript(this String val)
        {
            return HttpUtility.JavaScriptStringEncode(val ?? String.Empty);
        }
        public static string ToJson(this object val)
        {
            return JsonConvert.SerializeObject(val, Formatting.Indented, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ObjectCreationHandling = ObjectCreationHandling.Reuse });
        }
        public static String GetAppSetings(string key, string defaultVal = "")
        {
            try
            {
                var value = System.Configuration.ConfigurationManager.AppSettings[key].ToSafeString();
                var result = String.IsNullOrEmpty(value) ? defaultVal : value;
                return result;
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        //public static String GetAppSetings(string key, string defaultVal = "")
        //{
        //    try
        //    {
        //        var value = System.Configuration.ConfigurationManager.AppSettings[key].ToSafeString();
        //        var result = String.IsNullOrEmpty(value) ? defaultVal : value;
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        return defaultVal;
        //    }
        //}
        public static bool TieneCaracteresEspeciales(String palabra)
        {
            if (palabra.Any(x => !Char.IsLetterOrDigit(x)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static class SeparaPalabras
        {
            public enum TipoSeparacion
            {
                Defecto,
                Personalizado
            }
            /// <summary>
            /// Separa palabras, por defecto el separador será un espacio.
            /// </summary>
            /// <param name="palabras">grupo de palabras a separar</param>
            /// <returns></returns>
            public static String SepararPalabras(params String[] palabras)
            {
                return SepararPalabras(TipoSeparacion.Defecto, palabras);
            }
            /// <summary>
            /// Separa palabras, por defecto el separador será un espacio.
            /// </summary>
            /// <param name="tipoSeparacion">si el tipo de separación es Personalizado, entonces la primera palabra será el separador</param>
            /// <param name="palabras">grupo de palabras a separar</param>
            /// <returns></returns>
            public static String SepararPalabras(TipoSeparacion tipoSeparacion, params String[] palabras)
            {
                int posicion = 1;
                String separador = palabras[0] ?? "";
                if (tipoSeparacion == TipoSeparacion.Defecto)
                {
                    separador = " ";
                    posicion--;
                }
                String palabraSeparada = "";
                posicion++;
                if (palabras.Length > posicion)
                {
                    palabraSeparada = palabras[posicion];
                    for (int i = posicion; i < palabras.Length; i++)
                    {
                        palabraSeparada += separador + (palabras[i] ?? "");
                    }
                }
                return palabraSeparada;
            }
        }


        public static IDictionary<String, Object> ToObjectDictionary(this object val)
        {
            try
            {
                return val.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(prop => prop.Name, prop => prop.GetValue(val, null));
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }

        public static String ToDurationString(this Int32 val)
        {
            var hours = val / 60;
            var minutes = val % 60;
            var text = String.Empty;
            if (hours > 0)
                text += hours.ToString() + "h ";
            text += minutes.ToString("D2") + " m";
            return text;
        }

        public static Int32 ToInteger(this object val)
        {
            return ConvertHelpers.ToInteger(val, 0);
        }
        public static String ToSafeString(this object val)
        {
            return (val ?? String.Empty).ToString();
        }

        public static Int32 ToInteger(this object val, Int32 def)
        {
            try
            {
                Int32 reval = 0;

                if (Int32.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch (Exception ex)
            {
            }

            return def;
        }

        public static Decimal ToDecimal(this object val)
        {
            return ConvertHelpers.ToDecimal(val, 0);
        }

        public static Decimal ToDecimal(this object val, Decimal def)
        {
            try
            {
                Decimal reval = 0;

                if (Decimal.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch (Exception ex)
            {
            }

            return def;
        }

        public static long ToJsonDateTime(this DateTime val)
        {
            return ((long)(val - unixEpoch).TotalSeconds) * 1000;
        }

        public static String ToFullCallendarDate(this DateTime val)
        {
            return val.ToString("yyyy-MM-dd hh:mm:ss");
        }

        public static DateTime ToDateTime(this object val)
        {
            return ConvertHelpers.ToDateTime(val, DateTime.MinValue);
        }

        /// <summary>
        /// Convierte una fecha UTC a una fecha "Local". Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static DateTime ToLocalDate(this DateTime val)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(val, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
        }

        /// <summary>
        /// Convierte una fecha UTC a una fecha "Local". Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static DateTime? ToLocalDate(this DateTime? val)
        {
            if (val.HasValue)
                return ToLocalDate(val.Value);
            else
                return val;
        }

        /// <summary>
        /// Convierte una fecha UTC a una string de una fecha "Local" con formato. Se toma la hora de Per� (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <param name="format">Formato para el string</param>
        /// <returns>Fecha, en hora de Peru</returns>
        public static String ToLocalDateFormat(this DateTime val, String format)
        {
            return val.ToLocalDate().ToString(format, System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convierte una fecha UTC a una string de una fecha "Local" con formato. Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <param name="format">Formato para el string</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static String ToLocalDateString(this DateTime val, String format)
        {
            return val.ToLocalDate().ToString(format);
        }

        /// <summary>
        /// Convierte una fecha UTC a una string de una fecha "Local" con formato. Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <param name="format">Formato para el string</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static String ToLocalDateString(this DateTime? val, String format)
        {
            if (val.HasValue)
                return ToLocalDateString(val.Value, format);
            else
                return "";
        }

        public static DateTime ToDateTime(this object val, DateTime def)
        {
            try
            {
                DateTime reval = DateTime.MinValue;

                if (DateTime.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch (Exception ex)
            {
            }

            return def;
        }

        public static Boolean IsBetween(this DateTime val, DateTime Start, DateTime End)
        {
            return val >= Start && val <= End;
        }

        public static Boolean ToBoolean(this object val)
        {
            return ConvertHelpers.ToBoolean(val, false);
        }

        public static T ToEnum<T>(this String value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static Boolean ToBoolean(this object val, Boolean def)
        {
            try
            {
                Boolean reval = false;

                if (Boolean.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch (Exception ex)
            {
            }

            return def;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static dynamic EvaluateExpression(string eqn, dynamic valor)
        {
            var result = new object();
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                result = dt.Compute(eqn, string.Empty);

            }
            catch
            {
                result = valor;
            }
            return result;
        }
    }

    public static class PeruDateTime
    {
        public static DateTime Now
        {
            get
            {
                return (TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time")));
            }

        }
    }


}