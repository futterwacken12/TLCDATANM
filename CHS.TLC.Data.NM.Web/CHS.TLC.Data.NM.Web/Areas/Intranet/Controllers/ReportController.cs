using CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Report;
using CHS.TLC.Data.NM.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Intranet/Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult KardexValorized(String FechaInicio, String FechaFin, Int32? StockProductId)
        {
            var model = new KardexValorizedViewModel();
            model.Fill(CargarDatosContext(), FechaInicio, FechaFin, StockProductId);
            return View(model);
        }
    }
}