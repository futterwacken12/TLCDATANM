using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class _DeleteProductViewModel
    {
        public Int32? ProductId { get; set; }
        public String InvoiceDescription { get; set; }
        public _DeleteProductViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? productId)
        {
            this.ProductId = productId;

            if (this.ProductId.HasValue)
            {
                var product = c.context.Product.FirstOrDefault(x => x.ProductId == this.ProductId);

                this.InvoiceDescription = product.InvoiceDescription;
            }
        }
    }
}