using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class _DeleteContactViewModel
    {
        public Int32? ContactId { get; set; }
        public Int32 SupplierId { get; set; }
        public String FullName { get; set; }
        public _DeleteContactViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? contactId)
        {
            this.ContactId = ContactId;

            if (this.ContactId.HasValue)
            {
                var contact = c.context.Contact.FirstOrDefault(x => x.ContactId == this.ContactId);

                this.FullName = contact.Name + " " + contact.LastName + " " + contact.SecondLastName;
                this.SupplierId = contact.SupplierId;
            }
        }
    }
}