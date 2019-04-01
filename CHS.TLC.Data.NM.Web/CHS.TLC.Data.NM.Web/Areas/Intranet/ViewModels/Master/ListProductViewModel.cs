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
        public Int32? FamilyId { get; set; }
        public Int32? SubFamilyId { get; set; }
        public IPagedList<Product> LstProduct { get; set; }
        public List<Family> LstFamily { get; set; } = new List<Family>();
        public List<SubFamily> LstSubFamily { get; set; } = new List<SubFamily>();
        public Int32? FatherId { get; set; }
        public ListProductViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? page, String invoiceDescription, Int32? familyId, Int32? subFamilyId, Int32? fatherId)
        {
            this.Page = page ?? 1;
            this.InvoiceDescription = invoiceDescription;
            this.FamilyId = familyId;
            this.SubFamilyId = subFamilyId;
            this.LstFamily = c.context.Family.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
            this.LstSubFamily = c.context.SubFamily.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
            this.FatherId = fatherId;

            var query = c.context.Product.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).AsQueryable();

            if (!String.IsNullOrEmpty(this.InvoiceDescription))
            {
                query = query.Where(x => x.InvoiceDescription.Equals(this.InvoiceDescription));
            }
            if (this.FamilyId.HasValue)
            {
                query = query.Where(x => x.SubFamily.Family.FamilyId == this.FamilyId);
            }
            if (this.SubFamilyId.HasValue)
            {
                query = query.Where(x => x.SubFamilyId == this.SubFamilyId);
            }

            this.LstProduct = query.OrderBy(x => x.InvoiceDescription).ToPagedList(this.Page.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}