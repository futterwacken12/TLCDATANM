using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Store
{
    public class _DeleteEntryNoteViewModel
    {
        public Int32 EntryNoteId { get; set; }
        public String Code { get; set; }
        public Int32? FatherId { get; set; }
        public void Fill(CargarDatosContext c, Int32 entryNoteId, Int32? fatherId)
        {
            this.EntryNoteId = entryNoteId;
            this.FatherId = fatherId;
            this.Code = c.context.EntryNote.FirstOrDefault( x => x.EntryNoteId == this.EntryNoteId).Code;
        }
    }
}