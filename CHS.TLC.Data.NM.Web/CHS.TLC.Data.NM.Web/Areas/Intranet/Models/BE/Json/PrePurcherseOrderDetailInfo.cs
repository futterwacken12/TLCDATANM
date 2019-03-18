using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.Models.BE.Json
{
    public class PrePurcherseOrderDetailInfo
    {
        public String productCode { get; set; }
        public String descriptionInvoice { get; set; }
        public Decimal quantity { get; set; }
        public Int32 prePurcherseOrderDetailId { get; set; }
    }
}