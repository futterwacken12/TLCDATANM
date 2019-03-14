using CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Document;
using CHS.TLC.Data.NM.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.Controllers
{
    public class DocumentController : BaseController
    {
        // GET: Intranet/Document
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListPrePO(Int32? Page)
        {
            var model = new ListPrePOViewModel();
            model.Fill(CargarDatosContext(), Page);
            return View(model);
        }
    }
}