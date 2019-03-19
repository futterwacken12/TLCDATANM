using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.Models.BE.Json
{
    public class PurcherseOrderInfo
    {
        public String descriptionLocal { get; set; }
        public String descriptionInvoice { get; set; }
        public String code { get; set; }
        public String family { get; set; }
        public String design { get; set; }
        public Decimal quantity { get; set; }
        public String unit { get; set; }
        public Int32 prePurcherseOrderDetailId { get; set; }
        public String supplierName { get; set; }
        public Int32 supplierId { get; set; }
        public String documentCode { get; set; }
        public Int32 productId { get; set; }
    }
}