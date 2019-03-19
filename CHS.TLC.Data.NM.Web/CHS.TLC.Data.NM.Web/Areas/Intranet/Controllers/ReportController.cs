using CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Report;
using CHS.TLC.Data.NM.Web.Controllers;
using CHS.TLC.Data.NM.Web.Filters;
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
        [EncryptedActionParameter]
        public ActionResult KardexValorized(String FechaInicio, String FechaFin, Int32? StockProductId, Int32? FatherId)
        {
            var model = new KardexValorizedViewModel();
            model.Fill(CargarDatosContext(), FechaInicio, FechaFin, StockProductId, FatherId);
            return View(model);
        }
    }
}