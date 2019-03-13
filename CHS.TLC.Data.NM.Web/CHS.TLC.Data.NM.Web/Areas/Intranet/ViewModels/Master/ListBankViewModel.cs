using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class ListBankViewModel
    {
        public Int32? Page { get; set; }
        public String Name { get; set; }
        public String BankType { get; set; }
        public IPagedList<Bank> LstBank { get; set; }
        public List<SelectListItem> LstBankType { get; set; } = new List<SelectListItem>();
        public ListBankViewModel()
        {
            this.LstBankType.Add(new SelectListItem { Value = ConstantHelpers.TIPOBANCO.NACIONAL, Text = "NACIONAL" });
            this.LstBankType.Add(new SelectListItem { Value = ConstantHelpers.TIPOBANCO.EXTRANJERO, Text = "EXTRANJERO" });
        }
        public void Fill(CargarDatosContext c, Int32? page, String bankType)
        {
            this.Page = page ?? 1;
            this.BankType = bankType;

            var query = c.context.Bank.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).AsQueryable();

            if (!String.IsNullOrEmpty(this.BankType))
            {
                query = query.Where(x => x.Type.Equals(this.BankType));
            }

            this.LstBank = query.OrderBy(x => x.Description).ToPagedList(this.Page.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}