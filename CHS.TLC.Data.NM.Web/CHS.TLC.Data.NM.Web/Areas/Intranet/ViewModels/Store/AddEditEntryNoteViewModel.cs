using CHS.TLC.Data.NM.Web.Helpers;
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
        public DateTime Date { get; set; } = DateTime.Now;
        public TimeSpan Time { get; set; }
        public String Code { get; set; }
        public Int32 MovementTypeId { get; set; }
        public String SupplierGuideNumber { get; set; }
        public String TransportGuideNumber { get; set; }
        public String Seal { get; set; }
        public TimeSpan TransportTime { get; set; }
        public int DestinationStoreId { get; set; }
        public int SupplierId { get; set; }
        public int DocumentId { get; set; }
        public String DocumentCode { get; set; }
        public Int32 DocumentTypeId { get; set; }
        public Int32 ProductId { get; set; }
        public List<Web.Models.Store> LstStore { get; set; } = new List<Web.Models.Store>();
        public List<MovementType> LstMovementType { get; set; } = new List<MovementType>();
        public List<DocumentType> LstDocumentType { get; set; } = new List<DocumentType>();
        public List<EntryNoteDetail> LstEntryNoteDetail { get; set; } = new List<EntryNoteDetail>();
        public List<Int32> LstProduct { get; set; } = new List<int>();
        public String NombreProveedor { get; set; }
        public Int32? FatherId { get; set; }
        public void Fill(CargarDatosContext c, Int32? entryNoteId, Int32? fatherId)
        {
            this.FatherId = fatherId;
            this.EntryNoteId = entryNoteId;
            this.LstMovementType = c.context.MovementType.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
            this.LstStore = c.context.Store.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
            this.LstDocumentType = c.context.DocumentType.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();

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
                this.DocumentCode = entryNote.DocumentCode;
                this.NombreProveedor = entryNote.Supplier.BussinessName;

                this.LstEntryNoteDetail = c.context.EntryNoteDetail.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO
                && x.EntryNoteId == this.EntryNoteId).ToList();

                this.LstProduct = LstEntryNoteDetail.Select(x => x.PrePurcherseOrderDetail.ProductId).Distinct().ToList();


            }
        }
    }
}