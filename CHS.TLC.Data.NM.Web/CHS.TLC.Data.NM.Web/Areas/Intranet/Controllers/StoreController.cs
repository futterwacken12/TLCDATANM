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
        public ActionResult LstOuputNote(Int32? p, String State, String Date)
        {
            var model = new LstOuputNoteViewModel();
            model.Fill(CargarDatosContext(), p, State, Date);
            return View(model);
        }
        public ActionResult AddEditOutputNote(Int32? OutputNoteId)
        {
            var model = new AddEditOutputNoteViewModel();
            model.Fill(CargarDatosContext(), OutputNoteId);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEditOutputNote(AddEditOutputNoteViewModel model)
        {
            try
            {
                return RedirectToAction("LstOuputNote");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        public ActionResult LstEntryNote(Int32? p, String State, String Date)
        {
            var model = new LstEntryNoteViewModel();
            model.Fill(CargarDatosContext(), p, State, Date);
            return View(model);
        }
    }
}