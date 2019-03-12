using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Store
{
    public class LstOuputNoteViewModel
    {
        public String State { get; set; }
        public String Date { get; set; }
        public IPagedList<OutputNote> LstOutPutNote { get; set; }
        public Int32? p { get; set; }
        public void Fill(CargarDatosContext c, Int32? p, String state, String date)
        {
            this.p = p ?? 1;
            this.State = state;
            this.Date = date;

            var query = c.context.OutputNote.AsQueryable();

            if (!String.IsNullOrEmpty(this.State))
            {
                query = query.Where(x => x.State == this.State);
            }

            if (!String.IsNullOrEmpty(this.Date))
            {
                var dtDate = this.Date.ToDateTime();
                query = query.Where(x => x.Date.Day == dtDate.Day && x.Date.Month == dtDate.Month && x.Date.Year == dtDate.Year);
            }

            LstOutPutNote = query.OrderByDescending(x => x.OutputNoteId).ToPagedList(this.p.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);

        }
    }
}