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
    
    public partial class TypeExistence
    {
        public TypeExistence()
        {
            this.Product = new HashSet<Product>();
        }
    
        public int TypeExistenceId { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
    
        public virtual ICollection<Product> Product { get; set; }
    }
}
