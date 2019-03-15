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
    
    public partial class PrePurcherseOrder
    {
        public PrePurcherseOrder()
        {
            this.PurcherseOrder = new HashSet<PurcherseOrder>();
            this.PrePurcherseOrderDetail = new HashSet<PrePurcherseOrderDetail>();
        }
    
        public int PrePurcherseOrderId { get; set; }
        public string Code { get; set; }
        public System.DateTime DateOrder { get; set; }
        public System.DateTime RegistrationDate { get; set; }
        public string State { get; set; }
        public int SupplierId { get; set; }
        public int CountryId { get; set; }
        public System.DateTime ShipmentDate { get; set; }
        public string Text { get; set; }
        public string SendSupply { get; set; }
        public string DocumentName { get; set; }
        public byte[] DocumentPath { get; set; }
        public string DocumentExtension { get; set; }
        public string DocumentContentType { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual ICollection<PurcherseOrder> PurcherseOrder { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<PrePurcherseOrderDetail> PrePurcherseOrderDetail { get; set; }
    }
}
