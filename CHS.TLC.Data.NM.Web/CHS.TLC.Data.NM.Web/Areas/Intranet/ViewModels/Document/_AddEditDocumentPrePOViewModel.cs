using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Document
{
    public class _AddEditDocumentPrePOViewModel
    {
        public HttpPostedFileBase File { get; set; }
        public Int32 PrePurcherseOrderId { get; set; }
        public String Code { get; set; }
        public byte[]  DocumentPath { get; set; }
        public String DocumentName { get; set; }
        public void Fill(CargarDatosContext c, Int32 prePurcherseOrderId)
        {
            this.PrePurcherseOrderId = prePurcherseOrderId;
            var prePo = c.context.PrePurcherseOrder.FirstOrDefault(x => x.PrePurcherseOrderId == this.PrePurcherseOrderId);

            this.Code = prePo.Code;
            this.DocumentName = prePo.DocumentName;
            this.DocumentPath = prePo.DocumentPath;
        }
    }
}