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
    
    public partial class Treasury
    {
        public Treasury()
        {
            this.PurcherseOrder = new HashSet<PurcherseOrder>();
        }
    
        public int TreasuryId { get; set; }
        public decimal Value { get; set; }
        public string State { get; set; }
    
        public virtual ICollection<PurcherseOrder> PurcherseOrder { get; set; }
    }
}
