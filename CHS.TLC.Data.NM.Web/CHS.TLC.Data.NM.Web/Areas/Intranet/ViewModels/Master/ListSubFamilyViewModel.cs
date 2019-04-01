using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class ListSubFamilyViewModel
    {
        public Int32? Page { get; set; }
        public String Description { get; set; }
        public IPagedList<SubFamily> LstSubFamily { get; set; }
        public Int32? FatherId { get; set; }
        public ListSubFamilyViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? page, String description, Int32? fatherid)
        {
            this.Page = page ?? 1;
            this.Description = description;
            this.FatherId = fatherid;

            var query = c.context.SubFamily.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).AsQueryable();

            if (!String.IsNullOrEmpty(this.Description))
            {
                query = query.Where(x => x.Description.Equals(this.Description));
            }

            this.LstSubFamily = query.OrderBy(x => x.Description).ToPagedList(this.Page.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}