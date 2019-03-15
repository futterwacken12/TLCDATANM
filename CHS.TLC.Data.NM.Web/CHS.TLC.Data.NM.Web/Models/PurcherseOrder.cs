//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CHS.TLC.Data.NM.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurcherseOrder
    {
        public int PurcherseOrderId { get; set; }
        public int DocumentTypeId { get; set; }
        public string Code { get; set; }
        public int PrePurcherseOrderId { get; set; }
        public System.DateTime RegistrationDate { get; set; }
        public int CurrencyId { get; set; }
        public int IncotermsId { get; set; }
        public decimal Total { get; set; }
        public int ToleranceId { get; set; }
        public string ToleranceRemark { get; set; }
        public string ShipmentSample { get; set; }
        public int TreasuryId { get; set; }
        public int PortId { get; set; }
        public string BeneficiarySupply { get; set; }
        public string BeneficiarySupplyAdress { get; set; }
        public string BeneficiaryBankAdress { get; set; }
        public string BankAccountnumber { get; set; }
        public string SwiftCode { get; set; }
        public string IntermediaryBank { get; set; }
        public string SwiftIntermediaryBank { get; set; }
        public string CEOEmail { get; set; }
        public string SupplierEmail { get; set; }
        public string State { get; set; }
        public System.DateTime OrderDate { get; set; }
    
        public virtual Currency Currency { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual Incoterms Incoterms { get; set; }
        public virtual Port Port { get; set; }
        public virtual PrePurcherseOrder PrePurcherseOrder { get; set; }
        public virtual Tolerance Tolerance { get; set; }
        public virtual Treasury Treasury { get; set; }
    }
}
