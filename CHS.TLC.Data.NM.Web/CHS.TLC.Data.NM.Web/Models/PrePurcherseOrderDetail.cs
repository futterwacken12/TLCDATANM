//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CHS.TLC.Data.NM.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PrePurcherseOrderDetail
    {
        public int PrePurcherseOrderDetailId { get; set; }
        public int PrePurcherseOrderId { get; set; }
        public decimal Quantity { get; set; }
        public int MeasureUnitId { get; set; }
        public int ProductId { get; set; }
        public string State { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public virtual MeasureUnit MeasureUnit { get; set; }
        public virtual PrePurcherseOrder PrePurcherseOrder { get; set; }
        public virtual Product Product { get; set; }
    }
}