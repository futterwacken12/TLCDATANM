using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class ListProductViewModel
    {
        public Int32? Page { get; set; }
        public String InvoiceDescription { get; set; }
        public IPagedList<Product> LstProduct { get; set; }
        public ListProductViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? page, String invoiceDescription)
        {
            this.Page = page ?? 1;
            this.InvoiceDescription = invoiceDescription;

            var query = c.context.Product.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).AsQueryable();

            if (!String.IsNullOrEmpty(this.InvoiceDescription))
            {
                query = query.Where(x => x.InvoiceDescription.Equals(this.InvoiceDescription));
            }

            this.LstProduct = query.OrderBy(x => x.InvoiceDescription).ToPagedList(this.Page.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}