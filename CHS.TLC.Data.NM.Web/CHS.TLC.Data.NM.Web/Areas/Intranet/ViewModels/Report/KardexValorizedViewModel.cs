using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Report
{
    public class kardexDetail
    {
        public String Fecha { get; set; }
        public Decimal StockInicial { get; set; }
        public Decimal Salida { get; set; }
        public Decimal StockFinal { get; set; }
        public String UnidadEntrada { get; set; }
        public String ValorPromedio { get; set; }
        public Decimal Debe { get; set; }
        public String InvoiceDescription { get; set; }
        public Decimal Haber { get; set; }
        public Decimal KardexValorizado { get; set; }
    }
    public class KardexValorizedViewModel
    {
        public String FechaInicio { get; set; }
        public String FechaFin { get; set; }
        public Int32? StockProductId { get; set; }
        public String DescriptionInvoice { get; set; }
        public List<StockProductDetail> LstStockProductDetail { get; set; } = new List<StockProductDetail>();
        public List<Web.Models.Store> LstStore { get; set; } = new List<Web.Models.Store>();
        public Int32? FatherId { get; set; }
        public Int32? StoreId { get; set; }
        public Dictionary<Int32,String> DStockProduct { get; set; } = new Dictionary<Int32, String>();
        public Dictionary<Int32, List<kardexDetail>> DSKardex { get; set; } = new Dictionary<Int32, List<kardexDetail>>();

        public void Fill(CargarDatosContext c, String fechaInicio, String fechaFin, Int32? stockProductId, Int32? fatherId, Int32? storeId)
        {
            this.StoreId = storeId;
            this.FatherId = fatherId;
            this.StockProductId = stockProductId;
            this.FechaInicio = fechaInicio;
            this.FechaFin = fechaFin;
            this.LstStore = c.context.Store.Where( x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();

            var query = c.context.StockProductDetail.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).AsQueryable();

            if (this.StoreId.HasValue)
            {
                query = c.context.StockProductDetail.Where(x => x.StockProduct.StoreId == this.StoreId).AsQueryable();

                if (!String.IsNullOrEmpty(this.FechaInicio))
                {
                    var dtInicio = String.IsNullOrEmpty(this.FechaInicio) ? DateTime.Now.Date : this.FechaInicio.ToDateTime().Date;
                    query = query.Where(x => x.Date >= dtInicio);
                }
                if (!String.IsNullOrEmpty(this.FechaFin))
                {
                    var dtFin = String.IsNullOrEmpty(this.FechaFin) ? DateTime.Now.Date : this.FechaFin.ToDateTime().Date;
                    dtFin = dtFin.AddHours(23);
                    query = query.Where(x => x.Date <= dtFin);
                }
                if (this.StockProductId.HasValue)
                {
                    query = query.Where(x => x.StockProductId == this.StockProductId);
                    this.DescriptionInvoice = c.context.StockProduct.FirstOrDefault(x => x.StockProductId == this.StockProductId).Product.InvoiceDescription.ToUpper();
                }

                LstStockProductDetail = query.ToList();

                DStockProduct = LstStockProductDetail.Select(x => new { x.StockProductId , x.StockProduct.Product.InvoiceDescription}).Distinct().ToDictionary( x => x.StockProductId, x => x.InvoiceDescription);

                Decimal? exist = 0; Decimal? saldo = 0; Decimal? costoMedio = 0; Int32 i = 1; String currency = String.Empty;
                foreach (var stockProduct in DStockProduct)
                {
                    DSKardex.Add(stockProduct.Key, new List<kardexDetail>());

                    foreach (var item in LstStockProductDetail.Where(x => x.StockProductId == stockProduct.Key))
                    {
                        kardexDetail kdetail = new kardexDetail();

                        if (String.IsNullOrEmpty(currency))
                        {
                            currency = (item.Currency != null ? item.Currency.Sign : String.Empty);
                        }


                        saldo += (item.Operation == ConstantHelpers.OPERATION.ENTRY ? (item.Value * (item.CUPrice ?? 0)) : ((-1) * item.Value * costoMedio));
                        exist = exist + (item.Value * (item.Operation == ConstantHelpers.OPERATION.ENTRY ? 1 : (-1)));

                       kdetail.Fecha = item.Date.ToString("dd/MM/yyyy");
                       kdetail.StockInicial = (item.Operation == ConstantHelpers.OPERATION.ENTRY ? item.Value : 0);
                       kdetail.Salida = (item.Operation == ConstantHelpers.OPERATION.OUTPUT ? item.Value : 0);
                       kdetail.StockFinal = exist.Value;
                       kdetail.UnidadEntrada = (item.Operation == ConstantHelpers.OPERATION.ENTRY ? (currency + " " + (item.CUPrice ?? 0).ToString()) : String.Empty);
                       kdetail.ValorPromedio = (item.Operation == ConstantHelpers.OPERATION.ENTRY ? String.Empty : (currency + " " + (costoMedio).Value.ToString("#,##0.00")));
                       kdetail.Debe = (item.Operation == ConstantHelpers.OPERATION.ENTRY ? (item.Value * (item.CUPrice ?? 0)) : 0);
                       kdetail.Haber = (item.Operation == ConstantHelpers.OPERATION.ENTRY ? 0 : (item.Value * costoMedio.Value));
                        kdetail.KardexValorizado = saldo.Value;
                       kdetail.InvoiceDescription = stockProduct.Value;

                        if (item.Operation == ConstantHelpers.OPERATION.ENTRY)
                        {
                            i++;
                        }
                        if (item.Operation == ConstantHelpers.OPERATION.ENTRY)
                        {
                            costoMedio = saldo / exist;
                        }

                        DSKardex[stockProduct.Key].Add(kdetail);
                    }
                }
                
            }
        }
    }
}