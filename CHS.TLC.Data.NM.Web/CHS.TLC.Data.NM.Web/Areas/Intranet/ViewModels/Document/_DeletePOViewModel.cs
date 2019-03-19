using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Document
{
    public class _DeletePOViewModel
    {
        public Int32 PurcherseOrderId { get; set; }
        public String Code { get; set; }
        public void Fill(CargarDatosContext c, Int32 purcherseOrderId)
        {
            this.PurcherseOrderId = purcherseOrderId;
            this.Code = c.context.PurcherseOrder.FirstOrDefault(x => x.PurcherseOrderId == this.PurcherseOrderId).Code;
        }
    }
}