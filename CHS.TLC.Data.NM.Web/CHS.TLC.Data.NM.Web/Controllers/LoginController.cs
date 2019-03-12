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

            }
            catch (Exception ex)
            {

            }

            return Dasboard();
        }
        public ActionResult Dasboard()
        {
            return RedirectToAction("DashboardAdministrator", "Home", new { Area = "Intranet" });
        }
    }
}