using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class _DeleteSupplierViewModel
    {
        public Int32? SupplierId { get; set; }
        public String BussinessName { get; set; }
        public _DeleteSupplierViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? supplierId)
        {
            this.SupplierId = supplierId;

            if (this.SupplierId.HasValue)
            {
                var supplier = c.context.Supplier.FirstOrDefault(x => x.SupplierId == this.SupplierId);

                this.BussinessName = supplier.BussinessName;
            }
        }
    }
}