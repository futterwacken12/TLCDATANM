using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Store
{
    public class AddEditOutputNoteViewModel
    {
        public Int32? OutputNoteId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public TimeSpan Time { get; set; }
        public string Code { get; set; }
        public int MovementTypeId { get; set; }
        public int DestinationStoreId { get; set; }
        public int OriginalStoreId { get; set; }
        public Int32? DocumentId { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Other { get; set; }
        public bool IsOfficeGuide { get; set; }
        public bool IsTransferGuide { get; set; }
        public Int32? DocumentTypeId { get; set; }
        public string DocumentCode { get; set; }
        public List<Web.Models.Store> LstStore { get; set; } = new List<Web.Models.Store>();
        public List<MovementType> LstMovementType { get; set; } = new List<MovementType>();
        public List<DocumentType> LstDocumentType { get; set; } = new List<DocumentType>();
        public List<Int32> LstProduct { get; set; } = new List<Int32>();
        public List<OutputNoteDetail> LstOutputNoteDetail { get; set; } = new List<OutputNoteDetail>();
        public void Fill(CargarDatosContext c, Int32? outputNoteId)
        {
            this.OutputNoteId = outputNoteId;
            this.LstStore = c.context.Store.Where( x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
            this.LstMovementType = c.context.MovementType.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
            this.LstDocumentType = c.context.DocumentType.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();

            if (this.OutputNoteId.HasValue)
            {
                var outputNote = c.context.OutputNote.FirstOrDefault(x => x.OutputNoteId == this.OutputNoteId);

                this.Date = outputNote.Date;
                this.Time = outputNote.Time;
                this.Code = outputNote.Code;
                this.MovementTypeId = outputNote.MovementTypeId;
                this.DestinationStoreId = outputNote.DestinationStoreId;
                this.OriginalStoreId = outputNote.OriginalStoreId;
                this.DocumentId = outputNote.DocumentId;
                this.DepartureDate = outputNote.DepartureDate;
                this.Other = outputNote.Other;
                this.IsOfficeGuide = outputNote.IsOfficeGuide;
                this.IsTransferGuide = outputNote.IsTransferGuide;
                //this.DocumentCode = outputNote.DocumentCode;
                this.LstOutputNoteDetail = c.context.OutputNoteDetail.Where(x => x.OutputNoteId == this.OutputNoteId && x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
                this.LstProduct = c.context.StockProductDetail.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO &&
                                  x.OutputNoteId == this.OutputNoteId).Select(x => x.StockProduct.ProductId).Distinct().ToList();
            }
        }
                
    }
}