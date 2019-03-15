//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
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
        public string DocumentPath { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual ICollection<PurcherseOrder> PurcherseOrder { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<PrePurcherseOrderDetail> PrePurcherseOrderDetail { get; set; }
    }
}
