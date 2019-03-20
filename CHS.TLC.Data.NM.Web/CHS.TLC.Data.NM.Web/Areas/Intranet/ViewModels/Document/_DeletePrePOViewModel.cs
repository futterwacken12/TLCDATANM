using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Document
{
    public class _DeletePrePOViewModel
    {
        public Int32 PrePurcherseOrderId { get; set; }
        public String Code { get; set; }
        public Int32? FatherId { get; set; }
        public void Fill(CargarDatosContext c, Int32 prePurcherseOrderId, Int32? fatherId)
        {
            this.FatherId = fatherId;
            this.PrePurcherseOrderId = prePurcherseOrderId;
            this.Code = c.context.PrePurcherseOrder.FirstOrDefault( x => x.PrePurcherseOrderId == this.PrePurcherseOrderId).Code;
        }
    }
}