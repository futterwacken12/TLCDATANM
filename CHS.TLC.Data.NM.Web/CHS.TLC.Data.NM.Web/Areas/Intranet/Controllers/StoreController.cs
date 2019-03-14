using CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Store;
using CHS.TLC.Data.NM.Web.Controllers;
using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
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
        public ActionResult ListOutputNote(Int32? Page, String State, String Date, Int32? MovementTypeId, String Code)
        {
            var model = new LstOuputNoteViewModel();
            model.Fill(CargarDatosContext(), Page, State, Date, MovementTypeId, Code);
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
                OutputNote outputNote = null;

                if (model.OutputNoteId.HasValue)
                {
                    outputNote = context.OutputNote.FirstOrDefault( x => x.OutputNoteId == model.OutputNoteId);
                }
                else
                {
                    outputNote = new OutputNote();
                    outputNote.State = ConstantHelpers.ESTADO.ACTIVO;
                    outputNote.Date = DateTime.Now;
                    outputNote.Time = DateTime.Now.TimeOfDay;
                }


                PostMessage(MessageType.Success);
                return RedirectToAction("ListOuputNote");
            }
            catch(Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.OutputNoteId);
                return View(model);
            }
        }
        public ActionResult ListEntryNote(Int32? Page, String State, String Date, Int32? MovementTypeId, String Code)
        {
            var model = new LstEntryNoteViewModel();
            model.Fill(CargarDatosContext(), Page, State, Date, MovementTypeId, Code);
            return View(model);
        }
        public ActionResult AddEditEntryNote(Int32? OutputNoteId)
        {
            var model = new AddEditEntryNoteViewModel();
            model.Fill(CargarDatosContext(), OutputNoteId);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEditEntryNote(AddEditEntryNoteViewModel model)
        {
            try
            {
                EntryNote entryNote = null;

                if (model.EntryNoteId.HasValue)
                {
                    entryNote = context.EntryNote.FirstOrDefault( x => x.EntryNoteId == model.EntryNoteId);
                }
                else
                {
                    entryNote = new EntryNote();
                    entryNote.State = ConstantHelpers.ESTADO.ACTIVO;
                    entryNote.Date = DateTime.Now;
                    entryNote.Time = DateTime.Now.TimeOfDay;
                }


                PostMessage(MessageType.Success);
                return RedirectToAction("ListEntryNote");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.EntryNoteId);
                return View(model);
            }
        }
    }
}