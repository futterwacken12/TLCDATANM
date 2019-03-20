using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Document
{
    public class AddEditPrePOViewModel
    {
        public List<Supplier> LstSupplier { get; set; } = new List<Supplier>();
        public Int32? PrePurcherseOrderId { get; set; }
        public String Code { get; set; }
        public String DateOrder { get; set; }
        public String Registration { get; set; }
        public String RegistrationDate { get; set; }
        public String RegistrationTime { get; set; }
        public Int32 SupplierId { get; set; }
        public String SupplierBussinessName { get; set; }
        public String SupplierCode { get; set; }
        public String AddressSupplier { get; set; }
        public Int32 CountryId { get; set; }
        public String ShipmentDate { get; set; }
        public String Text { get; set; }
        public String SendSupply { get; set; }
        public String ProductId { get; set; }
        public List<Country> LstCountry { get; set; } = new List<Country>();
        public List<MeasureUnit> LstMeasureUnit { get; set; } = new List<MeasureUnit>();
        public Int32? FatherId { get; set; }
        public List<PrePurcherseOrderDetail> LstPrePODetail { get; set; } = new List<PrePurcherseOrderDetail>();
        public void Fill(CargarDatosContext c, Int32? prePurcherseOrderId, Int32? fatherId)
        {
            this.FatherId = fatherId;
            var fecha = DateTime.Now;
            this.Registration = fecha.ToString("dd/MM/yyyy HH:mm:ss");
            this.RegistrationDate = fecha.ToString("dd/MM/yyyy");
            this.RegistrationTime = fecha.ToString("HH:mm:ss");

            this.PrePurcherseOrderId = prePurcherseOrderId;
            //this.LstSupplier = c.context.Supplier.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
            this.LstCountry = c.context.Country.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
            this.LstMeasureUnit = c.context.MeasureUnit.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
            if (this.PrePurcherseOrderId.HasValue)
            {
                var prepo = c.context.PrePurcherseOrder.FirstOrDefault(x => x.PrePurcherseOrderId == this.PrePurcherseOrderId);
                this.LstPrePODetail = c.context.PrePurcherseOrderDetail.Where( x => x.PrePurcherseOrderId == this.PrePurcherseOrderId && x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
                this.Code = prepo.Code;
                this.DateOrder = prepo.DateOrder.ToString("dd/MM/yyyy");
                this.Registration = prepo.RegistrationDate.ToString("dd/MM/yyyy HH:mm:ss");
                this.SupplierId = prepo.SupplierId;
                this.CountryId = prepo.CountryId;
                this.ShipmentDate = prepo.ShipmentDate.ToString("dd/MM/yyyy");
                this.RegistrationDate = prepo.RegistrationDate.ToString("dd/MM/yyyy");
                this.RegistrationTime = prepo.RegistrationDate.ToString("HH:mm:ss");
                this.Text = prepo.Text;
                this.SendSupply = prepo.SendSupply;
                this.SupplierCode = prepo.Supplier.Code;
                this.AddressSupplier = prepo.Supplier.Address;
                this.SupplierBussinessName = prepo.Supplier.BussinessName;
            }
        }
    }
}