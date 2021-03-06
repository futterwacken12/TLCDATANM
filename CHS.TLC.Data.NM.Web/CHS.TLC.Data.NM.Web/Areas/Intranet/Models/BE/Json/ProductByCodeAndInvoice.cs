﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.Models.BE.Json
{
    public class ProductByCodeAndInvoice
    {
        public Int32 id { get; set; }
        public String text { get; set; }
        public String code { get; set; }
        public String invoiceDescription { get; set; }
        public String measureUnit { get; set; }
        public Decimal? quantity { get; set; }
    }
}