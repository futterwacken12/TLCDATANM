using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class ListContactViewModel
    {
        public Int32? SupplierId { get; set; }
        public List<Contact> LstContact { get; set; }
        public ListContactViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? supplierId)
        {
            this.SupplierId = supplierId;

            var query = c.context.Contact.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO) && x.SupplierId == this.SupplierId).AsQueryable();
            this.LstContact = query.OrderBy(x => x.LastName).ToList();
        }
    }
}