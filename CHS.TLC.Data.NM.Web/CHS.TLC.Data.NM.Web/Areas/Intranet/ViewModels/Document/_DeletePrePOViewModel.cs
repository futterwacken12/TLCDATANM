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
        public void Fill(CargarDatosContext c, Int32 prePurcherseOrderId)
        {
            this.PrePurcherseOrderId = prePurcherseOrderId;
            this.Code = c.context.PrePurcherseOrder.FirstOrDefault( x => x.PrePurcherseOrderId == this.PrePurcherseOrderId).Code;
        }
    }
}