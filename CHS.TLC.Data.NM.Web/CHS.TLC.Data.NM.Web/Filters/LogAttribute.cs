using CHS.TLC.Data.NM.Web.Models;
using CHS.TLC.Data.NM.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CHS.TLC.Data.NM.Web.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        public ViewContext viewContext { get; set; } = new ViewContext();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            this.Log(filterContext.RouteData, filterContext.HttpContext);
        }
        private void Log(RouteData routeData, HttpContextBase httpContext)
        {
            try
            {
                //if (!(routeData.Values["action"].ToString() != "KeepAlive"))
                //    return;
                //LogRequest logRequest = new LogRequest();
                //logRequest.IP = httpContext.Request.UserHostAddress ?? "";
                //logRequest.Type = httpContext.Request.RequestType ?? "";
                //logRequest.FormData = this.GetFormData(httpContext) ?? "";
                //logRequest.QueryString = this.GetQueryString(httpContext) ?? "";
                //logRequest.Area = routeData.DataTokens["area"] == null ? String.Empty : routeData.DataTokens["area"].ToString();
                //logRequest.Controller = routeData.Values["controller"].ToString();
                //logRequest.Action = routeData.Values["action"].ToString();
                //logRequest.Fecha = DateTime.Now;
                //logRequest.AllRaw = httpContext.Request.ServerVariables["ALL_RAW"] ?? "";
                //logRequest.SessionID = httpContext.Session.SessionID ?? "";
                //logRequest.Rol = httpContext.Session.GetRolAcronimo() ?? "";
                //if (httpContext.Session["UsuarioId"] != null)
                //    logRequest.UsuarioId = new int?(httpContext.Session.GetUsuarioId());
                //STKEntities stkEntities = new STKEntities();
                //stkEntities.LogRequest.Add(logRequest);
                //stkEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GetFormData(HttpContextBase context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < context.Request.Form.Count; ++index)
            {
                try
                {
                    stringBuilder.AppendFormat("{0}:{1}\r\n", (object)context.Request.Form.Keys[index], (object)context.Request.Form[index]);
                }
                catch (Exception ex)
                {
                }
            }
            if (context.Request.RequestType == "POST")
            {
                for (int index = 0; index < context.Request.Files.Count; ++index)
                {
                    try
                    {
                        stringBuilder.AppendFormat("FileName:{0}\r\nContentType:{1}\r\nContentLength:{2}\r\n", (object)context.Request.Files[index].FileName, (object)context.Request.Files[index].ContentType, (object)context.Request.Files[index].ContentLength);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return stringBuilder.ToString();
        }
        private string GetQueryString(HttpContextBase context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < context.Request.QueryString.Count; ++index)
            {
                try
                {
                    stringBuilder.AppendFormat("{0}:{1}\r\n", (object)context.Request.QueryString.Keys[index], (object)context.Request.QueryString[index]);
                }
                catch (Exception ex)
                {
                }
            }
            return stringBuilder.ToString();
        }
    }
}