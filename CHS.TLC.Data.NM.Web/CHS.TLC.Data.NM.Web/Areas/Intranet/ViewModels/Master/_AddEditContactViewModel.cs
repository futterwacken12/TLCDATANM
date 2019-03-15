using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class _AddEditContactViewModel
    {
        public Int32? ContactId { get; set; }
        public Int32 SupplierId { get; set; }
        public Int32 ItemId { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public String SecondLastName { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String State { get; set; }
        public List<Item> LstItem { get; set; } = new List<Item>();
        public _AddEditContactViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? contactId, Int32 supplierId)
        {
            this.ContactId = contactId;
            this.SupplierId = supplierId;
            this.LstItem = c.context.Item.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).ToList();

            if (this.ContactId.HasValue)
            {
                var contact = c.context.Contact.FirstOrDefault(x => x.ContactId == this.ContactId);

                this.SupplierId = contact.SupplierId;
                this.ItemId = contact.ItemId;
                this.Name = contact.Name;
                this.LastName = contact.LastName;
                this.SecondLastName = contact.SecondLastName;
                this.Phone = contact.Phone;
                this.Email = contact.Email;
                this.State = contact.State;
            }
        }
    }
}