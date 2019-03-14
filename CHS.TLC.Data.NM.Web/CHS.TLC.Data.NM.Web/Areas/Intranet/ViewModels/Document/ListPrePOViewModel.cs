using CHS.TLC.Data.NM.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Document
{
    public class ListPrePOViewModel
    {
        public Int32? Page { get; set; }
        public IPagedList<PrePurcherseOrder> LstPrePO { get; set; }
        public void Fill(CargarDatosContext c, Int32? page)
        {
            this.Page = page;

        }
    }
}