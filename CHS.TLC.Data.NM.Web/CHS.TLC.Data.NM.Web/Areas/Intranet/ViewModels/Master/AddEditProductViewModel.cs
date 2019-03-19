using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class AddEditProductViewModel
    {
        public Int32? ProductId { get; set; }
        public Int32? FamilyId { get; set; }
        public Int32? SubFamilyId { get; set; }
        public Int32 TypeExistenceId { get; set; }
        public Int32? MeasureUnitPrimaryId { get; set; }
        public Int32? MeasureUnitSecondaryId { get; set; }
        public Int32? SupplierId { get; set; }
        public String InternalCode { get; set; }
        public String SKUCode { get; set; }
        public Boolean IsActive { get; set; }
        public String Barcode { get; set; }
        public String DesignName { get; set; }
        public String NumberDesign { get; set; }
        public String Color { get; set; }
        public String InvoiceDescription { get; set; }
        public String LocalDescription { get; set; }
        public String LotNumberPurchase { get; set; }
        public Int32 TaxPercentageAdvalorenId { get; set; }
        public Int32 TaxPercentageAffectionIGVId { get; set; }
        public Int32 TaxPercentagePerceptionId { get; set; }
        public Int32 TaxPercentageDetractionId { get; set; }
        public Boolean IsSale { get; set; }
        public Boolean IsPurchase { get; set; }
        public String OtherMeasureUnit { get; set; }
        public String ConversionFactor { get; set; }
        public Int32? MinimumStock { get; set; }
        public String MinimumLot { get; set; }
        public String State { get; set; }
        public List<Family> LstFamily { get; set; } = new List<Family>();
        public List<SubFamily> LstSubFamily { get; set; } = new List<SubFamily>();
        public List<TypeExistence> LstTypeExistence { get; set; } = new List<TypeExistence>();
        public List<TaxPercentage> LstTaxPercentage { get; set; } = new List<TaxPercentage>();
        public List<MeasureUnit> LstMeasureUnit { get; set; } = new List<MeasureUnit>();
        public List<Supplier> LstSupplier { get; set; } = new List<Supplier>();
        public AddEditProductViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? productId)
        {
            this.ProductId = productId;
            this.LstFamily = c.context.Family.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).ToList();
            this.LstSubFamily = c.context.SubFamily.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).ToList();
            this.LstTypeExistence = c.context.TypeExistence.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).ToList();
            this.LstTaxPercentage = c.context.TaxPercentage.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).ToList();
            this.LstMeasureUnit = c.context.MeasureUnit.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).ToList();
            this.LstSupplier = c.context.Supplier.Where(x => x.State.Equals(ConstantHelpers.ESTADO.ACTIVO)).ToList();

            if (this.ProductId.HasValue)
            {
                var product = c.context.Product.FirstOrDefault(x => x.ProductId == this.ProductId);

                this.FamilyId = product.SubFamily.FamilyId;
                this.SubFamilyId = product.SubFamilyId;
                this.TypeExistenceId = product.TypeExistenceId;
                this.MeasureUnitPrimaryId = product.MeasureUnitPrimaryId;
                this.MeasureUnitSecondaryId = product.MeasureUnitSecondaryId;
                
                this.InternalCode = product.InternalCode;
                this.IsActive = product.IsActive;
                this.Barcode = product.Barcode;
                this.Color = product.Color;
                this.InvoiceDescription = product.InvoiceDescription;
                this.LocalDescription = product.LocalDescription;
                this.LotNumberPurchase = product.LotNumberPurchase;
                this.TaxPercentageAdvalorenId = product.TaxPercentageAdvalorenId;
                this.TaxPercentageAffectionIGVId = product.TaxPercentageAffectionIGVId;
                this.TaxPercentagePerceptionId = product.TaxPercentagePerceptionId;
                this.TaxPercentageDetractionId = product.TaxPercentageDetractionId;
                this.IsSale = product.IsSale.Value;
                this.IsPurchase = product.IsPurchase.Value;
                this.OtherMeasureUnit = product.OtherMeasureUnit;
                this.ConversionFactor = product.ConversionFactor;
                this.MinimumStock = product.MinimumStock;
                this.MinimumLot = product.MinimumLot;
                this.State = product.State;
            }
        }
    }
}