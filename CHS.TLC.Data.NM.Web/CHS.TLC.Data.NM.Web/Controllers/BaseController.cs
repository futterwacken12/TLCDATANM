using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CHS.TLC.Data.NM.Web.Filters;
using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;

namespace CHS.TLC.Data.NM.Web.Controllers
{
    [SessionRestore]
    public class BaseController : Controller
    {
        private CargarDatosContext cargarDatosContext;
        public TLCDataNMEntities context;
        
        public BaseController()
        {
            context = new TLCDataNMEntities();
        }
        public void InvalidarContext()
        {
            context = new TLCDataNMEntities();
        }
        public CargarDatosContext CargarDatosContext()
        {
            if (cargarDatosContext == null)
            {
                cargarDatosContext = new CargarDatosContext { context = context, session = Session, httpContext = HttpContext };
            }
            return cargarDatosContext;
        }
        public void PostMessage(FlashMessage Message)
        {
            if (TempData["FlashMessages"] == null)
                TempData["FlashMessages"] = new List<FlashMessage>();

            var list = ((List<FlashMessage>)TempData["FlashMessages"]);
            list.Add(Message);
        }
        public void PostMessage(MessageType Type, Boolean RemoveOnError = false)
        {
            String Body = "";

            switch (Type)
            {
                case MessageType.Error: Body = "Ha ocurrido un error al procesar la solicitud."; break;
                case MessageType.Info: Body = ""; break;
                case MessageType.Success: Body = "Los datos se guardaron exitosamente."; break;
                case MessageType.Warning: Body = "."; break;
            }
            PostMessage(Type, Body, RemoveOnError);
        }
        public void PostMessage(Exception ex, Boolean RemoveOnError = false)
        {
            PostMessage(MessageType.Error, "Ha ocurrido un error al procesar la solicitud: " + ex.Message.ToSafeString(), RemoveOnError);
        }

        public void PostMessage(MessageType Type, String Title, String Body, Boolean RemoveOnError = false)
        {
            PostMessage(new FlashMessage { Title = Title, Body = Body, Type = Type, RemoveOnError = RemoveOnError });
        }

        public void PostMessage(MessageType Type, String Body, Boolean RemoveOnError = false)
        {
            String Title = "";

            switch (Type)
            {
                case MessageType.Error: Title = "¡Error!"; break;
                case MessageType.Info: Title = "Ojo."; break;
                case MessageType.Success: Title = "¡Éxito!"; break;
                case MessageType.Warning: Title = "¡Atención!"; break;
            }

            PostMessage(new FlashMessage { Title = Title, Body = Body, Type = Type, RemoveOnError = RemoveOnError });
        }
        public ActionResult Error(Exception ex)
        {
            return View("Error", ex);
        }

        public ActionResult RedirectToActionPartialView(String actionName)
        {
            return RedirectToActionPartialView(actionName, null, null);
        }

        public ActionResult RedirectToActionPartialView(String actionName, object routeValues)
        {
            return RedirectToActionPartialView(actionName, null, routeValues);
        }

        public ActionResult RedirectToActionPartialView(String actionName, String controllerName)
        {
            return RedirectToActionPartialView(actionName, controllerName, null);
        }

        public ActionResult RedirectToActionPartialView(String actionName, String controllerName, object routeValues)
        {
            var url = Url.Action(actionName, controllerName, routeValues);
            return Content("<script> window.location = '" + url + "'</script>");
        }
        public ActionResult RedirectToActionSecure(string action, string controller, object args)
        {
            string queryString = string.Empty;

            if (args != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(args);

                for (int i = 0; i < d.Keys.Count; i++)
                {
                    if (i > 0)
                    {
                        queryString += "?";
                    }

                    queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            var encText = EncryptedExtensions.Encrypt(queryString);

            return RedirectToAction(action, controller, new { q = encText });
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.Params == filterContext.ActionParameters)
            {
            }

            base.OnActionExecuting(filterContext);
        }
        public List<RoleOption> ObtenerPermisos(Int32? RolId)
        {
            if (RolId.HasValue)
            {
                return context.RoleOption.Where(x => x.RoleId == RolId && x.State.Equals(ConstantHelpers.ESTADO.ACTIVO) && x.Option.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).OrderBy(x => x.Option.Order).ToList();
            }

            return null;
        }
        public String GenerarMenu(List<RoleOption> LstRoleOption)
        {
            var LstOption = LstRoleOption.OrderBy(x => x.Option.Order).Where(x => (!x.Option.FatherId.HasValue || x.Option.FatherId == 0) && x.Option.IsVisible && x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).Select(x => x.Option).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var option in LstOption)
                GenerarMenu(ref sb, option, LstRoleOption);

            return sb.ToString();
        }
        private void GenerarMenu(ref StringBuilder sb, Option option, List<RoleOption> LstRoleOption)
        {
            var lstPermission = LstRoleOption.Where(x => x.Option.FatherId.HasValue && x.Option.FatherId == option.OptionId && x.Option.IsVisible && x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).Select(x => x.Option);

            if (lstPermission != null && lstPermission.Count() > 0)
            {
                sb.Append("<li class=''>" +
                        "<a href='#' class='dropdown-collapse'><i class='icon-" + option.Icon + "'></i> <span>" + option.Description + "</span> <i class='icon-angle-down angle-down'></i></a>" +
                            "<ul class='nav nav-stacked'>");
                foreach (var permission in lstPermission)
                    GenerarMenu(ref sb, permission, LstRoleOption);

                sb.Append("</ul></li>");
            }
            else
            {
                sb.Append("<li class=''>" +
                              "<a href='" + Url.Action(option.Action, option.Controller, new { Area = option.Area }) + "'><i class='icon-" + option.Icon + "'></i> <span>" + option.Description + "</span></a>" +
                              "</li>");
            }
        }
    }
}