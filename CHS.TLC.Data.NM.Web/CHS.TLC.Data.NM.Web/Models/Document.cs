//------------------------------------------------------------------------------
// <auto-generated>
<<<<<<< HEAD
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
=======
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
>>>>>>> 57d2163f53fbd8daa250e31d80afb12de6273d18
// </auto-generated>
//------------------------------------------------------------------------------

namespace CHS.TLC.Data.NM.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Document
    {
        public Document()
        {
            this.EntryNote = new HashSet<EntryNote>();
            this.OutputNote = new HashSet<OutputNote>();
        }
    
        public int DocumentId { get; set; }
        public int DocumentTypeId { get; set; }
        public string State { get; set; }
        public string DocumentNumber { get; set; }
    
        public virtual DocumentType DocumentType { get; set; }
        public virtual ICollection<EntryNote> EntryNote { get; set; }
        public virtual ICollection<OutputNote> OutputNote { get; set; }
    }
}
