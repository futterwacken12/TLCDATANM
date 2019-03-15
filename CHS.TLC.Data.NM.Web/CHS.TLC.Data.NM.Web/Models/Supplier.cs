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
    
    public partial class Supplier
    {
        public Supplier()
        {
            this.Contact = new HashSet<Contact>();
            this.EntryNote = new HashSet<EntryNote>();
            this.PrePurcherseOrder = new HashSet<PrePurcherseOrder>();
            this.Product = new HashSet<Product>();
        }
    
        public int SupplierId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string Code { get; set; }
        public string RUC { get; set; }
        public string BussinessName { get; set; }
        public string MethodPayment { get; set; }
        public bool IsActive { get; set; }
        public string Provenance { get; set; }
        public string Origin { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public bool Retention { get; set; }
        public bool Detraction { get; set; }
        public bool Perception { get; set; }
        public string State { get; set; }
    
        public virtual ICollection<Contact> Contact { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<EntryNote> EntryNote { get; set; }
        public virtual ICollection<PrePurcherseOrder> PrePurcherseOrder { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
