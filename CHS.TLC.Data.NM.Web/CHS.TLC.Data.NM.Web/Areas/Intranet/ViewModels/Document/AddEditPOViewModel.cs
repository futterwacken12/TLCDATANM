using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Document
{
    public class AddEditPOViewModel
    {
        public Int32? PurcherseOrderId { get; set; }
        public String Code { get; set; }
        public String PreOrderCode { get; set; }
        public Int32? PrePurcherseOrderId { get; set; }
        public String RegistrationDate { get; set; }
        public Int32 CurrencyId { get; set; }
        public Int32 IncotermsId { get; set; }
        public Decimal Total { get; set; }
        public Int32 CountryId { get; set; }
        public Int32 ToleranceId { get; set; }
        public String ToleranceRemark { get; set; }
        public String ShipmentSample { get; set; }
        public Int32 TreasuryId { get; set; }
        public Int32 PortId { get; set; }
        public String BeneficiarySupply { get; set; }
        public String BeneficiarySupplyAdress { get; set; }
        public String BeneficiaryBankAdress { get; set; }
        public String BankAccountnumber { get; set; }
        public String SwiftCode { get; set; }
        public String IntermediaryBank { get; set; }
        public String SwiftIntermediaryBank { get; set; }
        public String CEOEmail { get; set; }
        public String SupplierEmail { get; set; }
        public String ShipmentDate {get; set;}
        public String MethodPayment { get; set; }
        public String OrderDate { get; set; }
        public List<Currency> LstCurrency = new List<Currency>();
        public List<Incoterms> LstIncoterms = new List<Incoterms>();
        public List<PrePurcherseOrderDetail> LstPrePODetail = new List<PrePurcherseOrderDetail>();
        public List<Tolerance> LstTolerance = new List<Tolerance>();
        public List<Country> LstCountry = new List<Country>();
        public List<Treasury> LstTreasury = new List<Treasury>();
        public List<Port> LstPort = new List<Port>();
        public String CurrencyCode = String.Empty;
        public List<SelectListItem> LstMethodPayment = new List<SelectListItem>();

        public void Fill(CargarDatosContext c, Int32? purcherseOrderId,Int32? prePurcherseOrderId)
        {
            this.PrePurcherseOrderId = prePurcherseOrderId;
            this.PurcherseOrderId = purcherseOrderId;
            if (this.PrePurcherseOrderId.HasValue)
            {
                this.PreOrderCode = c.context.PrePurcherseOrder.FirstOrDefault(x => x.PrePurcherseOrderId == this.PrePurcherseOrderId).Code;
            }
            this.RegistrationDate = DateTime.Now.ToString("dd/MM/yyyy");
            this.LstCurrency = c.context.Currency.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
            this.LstIncoterms = c.context.Incoterms.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
            this.LstTolerance = c.context.Tolerance.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).OrderBy(x => x.Value).ToList();
            this.LstCountry = c.context.Country.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
            this.LstTreasury = c.context.Treasury.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
            this.LstPort = c.context.Port.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();

            this.LstMethodPayment.Add(new SelectListItem { Value = ConstantHelpers.FORMAPAGO.CREDITO, Text = "CRÉDITO" });
            this.LstMethodPayment.Add(new SelectListItem { Value = ConstantHelpers.FORMAPAGO.CONTADO, Text = "CONTADO" });

            if (this.PurcherseOrderId.HasValue)
            {
                var po = c.context.PurcherseOrder.Include(x => x.PrePurcherseOrder).Include(x => x.PrePurcherseOrder.Supplier).Include(x => x.PrePurcherseOrder.PrePurcherseOrderDetail)
                    .FirstOrDefault(x => x.PurcherseOrderId == this.PurcherseOrderId);

                this.Code = po.Code;
                this.PreOrderCode = po.PrePurcherseOrder.Code;
                this.PrePurcherseOrderId = po.PrePurcherseOrderId;
                this.RegistrationDate = po.RegistrationDate.ToString("dd/MM/yyyy");
                this.CurrencyId = po.CurrencyId;
                this.IncotermsId = po.IncotermsId;
                this.Total = po.Total;
                this.ToleranceId = po.ToleranceId;
                this.ToleranceRemark = po.ToleranceRemark;
                this.ShipmentSample = po.ShipmentSample;
                this.TreasuryId = po.TreasuryId;
                this.PortId = po.PortId;
                this.BeneficiarySupply = po.BeneficiarySupply;
                this.BeneficiarySupplyAdress = po.BeneficiarySupplyAdress;
                this.BeneficiaryBankAdress = po.BeneficiaryBankAdress;
                this.BankAccountnumber = po.BankAccountnumber;
                this.SwiftCode = po.SwiftCode;
                this.IntermediaryBank = po.IntermediaryBank;
                this.SwiftIntermediaryBank = po.SwiftIntermediaryBank;
                this.CEOEmail = po.CEOEmail;
                this.SupplierEmail = po.SupplierEmail;
                this.OrderDate = po.OrderDate.ToString("dd/MM/yyyy");
                this.MethodPayment = po.PrePurcherseOrder.Supplier.MethodPayment;
                this.CountryId = po.CountryId;
                this.ShipmentDate = po.ShipmentDate.ToString("dd/MM/yyyy");
                this.CurrencyCode = po.Currency.Name;
                this.LstPrePODetail = po.PrePurcherseOrder.PrePurcherseOrderDetail.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
            }
        }
    }
}