using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class ListFamilyViewModel
    {
        public Int32? Page { get; set; }
        public String Name { get; set; }
        public IPagedList<Family> ListFamily { get; set; }
        public ListFamilyViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? page)
        {
            this.Page = page ?? 1;

            var query = c.context.Family.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).AsQueryable();
            this.ListFamily = query.OrderBy(x => x.Description).ToPagedList(this.Page.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}