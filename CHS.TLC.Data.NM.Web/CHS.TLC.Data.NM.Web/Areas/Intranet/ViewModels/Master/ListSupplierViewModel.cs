using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class ListSupplierViewModel
    {
        public Int32? Page { get; set; }
        public String BussinessName { get; set; }
        public IPagedList<Supplier> LstSupplier { get; set; }
        public Int32? FatherId { get; set; }
        public ListSupplierViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? page, String bussinessName, Int32? fatherid)
        {
            this.Page = page ?? 1;
            this.BussinessName = bussinessName;
            this.FatherId = fatherid;

            var query = c.context.Supplier.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).AsQueryable();

            if (!String.IsNullOrEmpty(this.BussinessName))
            {
                query = query.Where(x => x.BussinessName.Equals(this.BussinessName));
            }

            this.LstSupplier = query.OrderBy(x => x.BussinessName).ToPagedList(this.Page.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}