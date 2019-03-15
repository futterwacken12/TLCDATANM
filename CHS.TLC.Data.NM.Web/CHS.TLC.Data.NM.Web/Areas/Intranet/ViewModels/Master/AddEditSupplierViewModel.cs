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
        public Int32? CountryId { get; set; }
        public String Code { get; set; }
        public String RUC { get; set; }
        public String BussinessName { get; set; }
        public Boolean IsActive { get; set; }
        public String Provenance { get; set; }
        public String MethodPayment { get; set; }
        public String Origin { get; set; }
        public String Address { get; set; }
        public String ZipCode { get; set; }
        public Boolean Retention { get; set; }
        public Boolean Detraction { get; set; }
        public Boolean Perception { get; set; }
        public String State { get; set; }
        public List<SelectListItem> LstMethodPayment { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> LstProvenance { get; set; } = new List<SelectListItem>();
        public List<Country> LstCountry { get; set; } = new List<Country>();
        public AddEditSupplierViewModel()
        {
            this.LstProvenance.Add(new SelectListItem { Value = ConstantHelpers.PROCEDENCIA.LOCAL, Text = "LOCAL" });
            this.LstProvenance.Add(new SelectListItem { Value = ConstantHelpers.PROCEDENCIA.INTERNACIONAL, Text = "INTERNACIONAL" });
            this.LstMethodPayment.Add(new SelectListItem { Value = ConstantHelpers.FORMAPAGO.CREDITO, Text = "CRÉDITO" });
            this.LstMethodPayment.Add(new SelectListItem { Value = ConstantHelpers.FORMAPAGO.CONTADO, Text = "CONTADO" });
        }
        public void Fill(CargarDatosContext c, Int32? supplierId)
        {
            this.SupplierId = supplierId;
            this.LstCountry = c.context.Country.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).ToList();

            if (this.SupplierId.HasValue)
            {
                var supplier = c.context.Supplier.FirstOrDefault(x => x.SupplierId == this.SupplierId);

                this.CountryId = supplier.CountryId;
                this.Code = supplier.Code;
                this.RUC = supplier.RUC;
                this.BussinessName = supplier.BussinessName;
                this.MethodPayment = supplier.MethodPayment;
                this.IsActive = supplier.IsActive;
                this.Provenance = supplier.Provenance;
                this.Origin = supplier.Origin;
                this.Address = supplier.Address;
                this.ZipCode = supplier.ZipCode;
                this.Retention = supplier.Retention;
                this.Detraction = supplier.Detraction;
                this.Perception = supplier.Perception;
                this.State = supplier.State;
            }
        }
    }
}