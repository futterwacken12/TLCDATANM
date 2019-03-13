using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Store
{
    public class AddEditEntryNoteViewModel
    {
        public Int32? EntryNoteId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public String Code { get; set; }
        public Int32 MovementTypeId { get; set; }
        public String SupplierGuideNumber { get; set; }
        public String TransportGuideNumber { get; set; }
        public String Seal { get; set; }
        public TimeSpan TransportTime { get; set; }
        public int DestinationStoreId { get; set; }
        public int TransferStoreId { get; set; }
        public int SupplierId { get; set; }
        public int DocumentId { get; set; }

        public void Fill(CargarDatosContext c, Int32? entryNoteId)
        {
            this.EntryNoteId = entryNoteId;

            if (this.EntryNoteId.HasValue)
            {
                var entryNote = c.context.EntryNote.FirstOrDefault(x => x.EntryNoteId == this.EntryNoteId);

                this.Date = entryNote.Date;
                this.Time = entryNote.Time;
                this.Code = entryNote.Code;
                this.MovementTypeId = entryNote.MovementTypeId;
                this.DestinationStoreId = entryNote.DestinationStoreId;
                this.SupplierGuideNumber = entryNote.SupplierGuideNumber;
                this.DocumentId = entryNote.DocumentId;
                this.TransportGuideNumber = entryNote.TransportGuideNumber;
                this.Seal = entryNote.Seal;
                this.TransportTime = entryNote.TransportTime;
                this.DestinationStoreId = entryNote.DestinationStoreId;
                this.SupplierId = entryNote.SupplierId;
                this.DocumentId = entryNote.DocumentId;
            }
        }
    }
}