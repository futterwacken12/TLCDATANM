using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.ViewModels.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    PostMessage(MessageType.Error, "Datos Incorrectos");
                    TryUpdateModel(model);
                    return View(model);
                }

                var passwordEncr = EncryptHelpers.Encriptar(model.Password);
                var Usuario = context.User.FirstOrDefault(x => x.UserName == model.UserName && (x.State == ConstantHelpers.ESTADO.ACTIVO));
                if (Usuario == null)
                {
                    PostMessage(MessageType.Warning, "Usuario y/o Contraseña inválidos.");
                    TryUpdateModel(model);
                    return View(model);
                }
                else
                {
                    if (Usuario.State == ConstantHelpers.ESTADO.DESACTIVADO)
                    {
                        PostMessage(MessageType.Error, "El usuario se encuentra desactivado. Por favor, contactarse con el Administrador.");
                        TryUpdateModel(model);
                        return View(model);
                    }                   
                }
                var password = Usuario.Password.FirstOrDefault(x => x.State == ConstantHelpers.ESTADO.ACTIVO
               && x.Value == passwordEncr);

                if (password == null)
                {
                    context.SaveChanges();
                    PostMessage(MessageType.Warning, "Usuario y/o Contraseña inválidos.");

                    TryUpdateModel(model);
                    return View(model);
                }

                Session.Set(SessionKey.Nombres, Usuario.Name + " " + Usuario.LastName);
                Session.Set(SessionKey.RolId, Usuario.RoleId);
                Session.Set(SessionKey.UsuarioId, Usuario.UserId);
                
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                TryUpdateModel(model);
                return View(model);
            }

            return Dashboard();
        }
        public ActionResult Dashboard()
        {
            return RedirectToActionSecureArea("MainOption", "Home", "Intranet", new { FatherId = 46 });
        }
        public ActionResult LogOut()
        {
            try
            {
                Session.Clear();
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login");
            }
        }
    }
}