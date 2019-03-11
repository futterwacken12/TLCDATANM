using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CHS.TLC.Data.NM.Web.Filters
{
    public static class EncryptedExtensions
    {
        private static byte[] secretKey = Encoding.UTF8.GetBytes("!R5_6Gfy4#2f?!fD");
        private static byte[] iv64 = { };
        private static String _iv { get; set; } = System.Configuration.ConfigurationManager.AppSettings["IV"];

        public static MvcHtmlString EncodedActionLink(this HtmlHelper htmlHelper, IHtmlString linkText, string actionName, string controllerName, string areaName, object routeValues, object htmlAttributes)
        {
            string queryString = string.Empty;
            string htmlAttributesString = string.Empty;
            string deployName = ConfigurationManager.AppSettings["Deploy_Name"];

            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    if (i > 0)
                    {
                        queryString += "?";
                    }
                    queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            if (htmlAttributes != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    htmlAttributesString += " " + d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            StringBuilder ancor = new StringBuilder();
            ancor.Append("<a ");
            if (htmlAttributesString != string.Empty)
            {
                ancor.Append(htmlAttributesString);
            }
            ancor.Append(" href='");
            ancor.Append("/" + deployName);

            if (areaName != string.Empty)
            {
                ancor.Append("/" + areaName);
            }
            if (controllerName != string.Empty)
            {
                ancor.Append("/" + controllerName);
            }
            if (actionName != "Index")
            {
                ancor.Append("/" + actionName);
            }
            if (queryString != string.Empty)
            {
                ancor.Append("?q=" + Encrypt(queryString));
            }
            ancor.Append("'");
            ancor.Append(">");
            ancor.Append(linkText);
            ancor.Append("</a>");

            return new MvcHtmlString(ancor.ToString());
        }
        public static MvcHtmlString EncodedActionLinkModal(this HtmlHelper htmlHelper, IHtmlString linkText, string actionName, string controllerName, string areaName, object routeValues, object htmlAttributes)
        {
            string queryString = string.Empty;
            string htmlAttributesString = string.Empty;
            string deployName = ConfigurationManager.AppSettings["Deploy_Name"];

            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    if (i > 0)
                    {
                        queryString += "?";
                    }
                    queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            if (htmlAttributes != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    htmlAttributesString += " " + d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            StringBuilder ancor = new StringBuilder();
            ancor.Append("<a ");
            if (htmlAttributesString != string.Empty)
            {
                ancor.Append(htmlAttributesString);
            }

            ancor.Append("data-type='modal-link' href='#' data-source-url='");
            ancor.Append("/" + deployName);

            if (areaName != string.Empty)
            {
                ancor.Append("/" + areaName);
            }
            if (controllerName != string.Empty)
            {
                ancor.Append("/" + controllerName);
            }
            if (actionName != "Index")
            {
                ancor.Append("/" + actionName);
            }
            if (queryString != string.Empty)
            {
                ancor.Append("?q=" + Encrypt(queryString));
            }
            ancor.Append("'");
            ancor.Append(">");
            ancor.Append(linkText);
            ancor.Append("</a>");

            return new MvcHtmlString(ancor.ToString());
        }
        public static string Encrypt(string plainText)
        {
            try
            {
                iv64 = System.Text.Encoding.UTF8.GetBytes(_iv);
                TripleDESCryptoServiceProvider cryptoSP = new TripleDESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoSP.CreateEncryptor(secretKey, iv64), CryptoStreamMode.Write);
                cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                cryptoStream.FlushFinalBlock();

                return Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message + " - " + (ex.InnerException != null ? ex.InnerException.Message : String.Empty);
            }
        }
    }
}