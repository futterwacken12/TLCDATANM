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
    
    public partial class Family
    {
        public Family()
        {
            this.SubFamily = new HashSet<SubFamily>();
        }
    
        public int FamilyId { get; set; }
        public string Description { get; set; }
        public string SKUCode { get; set; }
        public string State { get; set; }
    
        public virtual ICollection<SubFamily> SubFamily { get; set; }
    }
}
