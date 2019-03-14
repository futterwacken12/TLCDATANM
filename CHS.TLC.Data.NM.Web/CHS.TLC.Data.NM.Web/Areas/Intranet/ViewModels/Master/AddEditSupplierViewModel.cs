using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class AddEditSupplierViewModel
    {
        public Int32? SupplierId { get; set; }
        public Int32 CountryId { get; set; }
        public String Code { get; set; }
        public String RUC { get; set; }
        public String BussinessName { get; set; }
        public Boolean IsActive { get; set; }
        public String Provenance { get; set; }
        public String Origin { get; set; }
        public String Address { get; set; }
        public String ZipCode { get; set; }
        public Boolean Retention { get; set; }
        public Boolean Detraction { get; set; }
        public Boolean Perception { get; set; }
        public String State { get; set; }
        public List<SelectListItem> LstProvenance { get; set; } = new List<SelectListItem>();
        public AddEditSupplierViewModel()
        {
            this.LstProvenance.Add(new SelectListItem { Value = ConstantHelpers.TIPOBANCO.NACIONAL, Text = "LOCAL" });
            this.LstProvenance.Add(new SelectListItem { Value = ConstantHelpers.TIPOBANCO.EXTRANJERO, Text = "INTERNACIONAL" });
        }
        public void Fill(CargarDatosContext c, Int32? supplierId)
        {

        }
    }
}