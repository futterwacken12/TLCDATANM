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
        public Int32? FatherId { get; set; }
        public void Fill(CargarDatosContext c, String fechaInicio, String fechaFin, Int32? stockProductId, Int32? fatherId)
        {
            this.FatherId = fatherId;
            this.StockProductId = stockProductId;
            this.FechaInicio = fechaInicio;
            this.FechaFin = fechaFin;
            if (this.StockProductId.HasValue)
            {
                var query = c.context.StockProductDetail.Where(x => x.StockProductId == this.StockProductId).AsQueryable();

                this.DescriptionInvoice = c.context.StockProduct.FirstOrDefault( x => x.StockProductId == this.StockProductId).Product.InvoiceDescription.ToUpper();
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
                

                LstStockProductDetail = query.ToList();
            }
        }
    }
}