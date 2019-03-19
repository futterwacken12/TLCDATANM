using CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Home;
using CHS.TLC.Data.NM.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Intranet/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DashboardAdministrator()
        {
            var model = new DashboardAdministratorViewModel();
            return View(model);
        }
        public ActionResult MainOption(Int32? FatherId)
        {
            var model = new MainOptionViewModel();
            model.Fill(CargarDatosContext(), FatherId);
            return View(model);
        }
    }
}