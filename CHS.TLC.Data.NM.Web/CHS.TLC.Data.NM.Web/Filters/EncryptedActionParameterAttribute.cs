using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EncryptedActionParameterAttribute : ActionFilterAttribute
    {
        private static byte[] secretKey = Encoding.UTF8.GetBytes("!R5_6Gfy4#2f?!fD");
        private static byte[] iv64 = { };
        private static String _iv { get; set; } = System.Configuration.ConfigurationManager.AppSettings["IV"];

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Dictionary<string, object> decryptedParameters = new Dictionary<string, object>();

            if (HttpContext.Current.Request.QueryString.Get("q") != null)
            {
                string encryptedQueryString = HttpContext.Current.Request.QueryString.Get("q");
                string decrptedString = Decrypt(encryptedQueryString.ToString());
                string[] paramsArrs = decrptedString.Split('?');

                for (int i = 0; i < paramsArrs.Length; i++)
                {
                    string[] paramArr = paramsArrs[i].Split('=');
                    if(!String.IsNullOrEmpty(paramArr[1]))
                    {
                        decryptedParameters.Add(paramArr[0], Convert.ToInt32(paramArr[1]));
                    }
                }
            }

            for (int i = 0; i < decryptedParameters.Count; i++)
            {
                filterContext.ActionParameters[decryptedParameters.Keys.ElementAt(i)] = decryptedParameters.Values.ElementAt(i);
            }

            base.OnActionExecuting(filterContext);
        }

        private String Decrypt(String encryptedText)
        {
            encryptedText = encryptedText.Replace(" ", "+");
            iv64 = System.Text.Encoding.UTF8.GetBytes(_iv);
            TripleDESCryptoServiceProvider cryptoSP = new TripleDESCryptoServiceProvider();
            byte[] inputByteArray = Convert.FromBase64String(encryptedText);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoSP.CreateDecryptor(secretKey, iv64), CryptoStreamMode.Write);
            cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}