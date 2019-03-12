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
        public DateTime Date { get; set; }
        public TimeSpan Hour { get; set; }
        public string Code { get; set; }
        public int MovementTypeId { get; set; }
        public int DestinationStoreId { get; set; }
        public int OriginalStoreId { get; set; }
        public Int32? DocumentId { get; set; }
        public Int32? DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Other { get; set; }
        public bool IsOfficeGuide { get; set; }
        public bool IsTransferGuide { get; set; }

        public void Fill(CargarDatosContext c, Int32? outputNoteId)
        {
            this.OutputNoteId = outputNoteId;

            if (this.OutputNoteId.HasValue)
            {
                var outputNote = c.context.OutputNote.FirstOrDefault(x => x.OutputNoteId == this.OutputNoteId);

                this.Date = outputNote.Date;
                this.Hour = outputNote.Hour;
                this.Code = outputNote.Code;
                this.MovementTypeId = outputNote.MovementTypeId;
                this.DestinationStoreId = outputNote.DestinationStoreId;
                this.OriginalStoreId = outputNote.OriginalStoreId;
                this.DocumentId = outputNote.DocumentId;
                this.DocumentTypeId = outputNote.DocumentTypeId;
                this.DocumentNumber = outputNote.DocumentNumber;
                this.DepartureDate = outputNote.DepartureDate;
                this.Other = outputNote.Other;
                this.IsOfficeGuide = outputNote.IsOfficeGuide;
                this.IsTransferGuide = outputNote.IsTransferGuide;
            }
        }
    }
}