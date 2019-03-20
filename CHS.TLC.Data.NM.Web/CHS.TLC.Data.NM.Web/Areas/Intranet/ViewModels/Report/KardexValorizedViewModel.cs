using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Report
{
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
            }
        }
    }
}