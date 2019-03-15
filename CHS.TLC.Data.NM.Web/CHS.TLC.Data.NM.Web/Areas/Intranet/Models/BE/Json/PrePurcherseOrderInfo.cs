using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.Models.BE.Json
{
    public class PrePurcherseOrderInfo
    {
        public String orderDate { get; set; }
        public String supplierName { get; set; }
        public String supplierAddress { get; set; }
        public Int32 countryId { get; set; }
        public String supplierEmail { get; set; } 
        public String shipmentDate { get; set; }
        public Int32 supplierId { get; set; }
    }
}