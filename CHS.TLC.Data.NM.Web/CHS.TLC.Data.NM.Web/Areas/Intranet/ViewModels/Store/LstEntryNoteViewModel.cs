using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Store
{
    public class LstEntryNoteViewModel
    {
        [Display(Name = "Tipo de Movimiento")]
        public Int32? MovementTypeId { get; set; }
        public List<MovementType> LstMovementType { get; set; } = new List<MovementType>();
        [Display(Name = "Código")]
        public String Code { get; set; }
        public List<SelectListItem> LstState { get; set; } = new List<SelectListItem>();
        [Display(Name = "Estado")]
        public String State { get; set; }
        [Display(Name = "Fecha")]
        public String Date { get; set; }
        public IPagedList<EntryNote> LstEntryNote { get; set; }
        public Int32? Page { get; set; }

        public LstEntryNoteViewModel()
        {
            LstState.Add(new SelectListItem { Value = ConstantHelpers.ESTADO.ACTIVO, Text = "ACTIVO" });
            LstState.Add(new SelectListItem { Value = ConstantHelpers.ESTADO.INACTIVO, Text = "INACTIVO" });
        }

        public void Fill(CargarDatosContext c, Int32? page, String state, String date, Int32? movementTypeId, String code)
        {
            this.Page = page ?? 1;
            this.State = state;
            this.Date = date;
            this.Code = code;
            this.MovementTypeId = movementTypeId;

            this.LstMovementType = c.context.MovementType.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();

            var query = c.context.EntryNote.Where( x => x.State == ConstantHelpers.ESTADO.ACTIVO).AsQueryable();

            if (!String.IsNullOrEmpty(this.State))
            {
                query = query.Where(x => x.State == this.State);
            }
            if (!String.IsNullOrEmpty(this.Date))
            {
                var dtDate = this.Date.ToDateTime();
                query = query.Where(x => x.Date.Day == dtDate.Day && x.Date.Month == dtDate.Month && x.Date.Year == dtDate.Year);
            }
            if (this.MovementTypeId.HasValue)
            {
                query = query.Where(x => x.MovementTypeId == this.MovementTypeId);
            }
            if (!String.IsNullOrEmpty(this.Code))
            {
                query = query.Where(x => x.Code == this.Code);
            }


            LstEntryNote = query.OrderByDescending(x => x.EntryNoteId).ToPagedList(this.Page.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);

        }
    }
}