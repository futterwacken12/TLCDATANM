using CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Store;
using CHS.TLC.Data.NM.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.Controllers
{
    public class StoreController : BaseController
    {
        // GET: Intranet/Store
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LstOuputNote()
        {
            var model = new LstOuputNoteViewModel();
            return View(model);
        }
        public ActionResult LstEntryNote()
        {
            var model = new LstEntryNoteViewModel();
            return View(model);
        }
    }
}