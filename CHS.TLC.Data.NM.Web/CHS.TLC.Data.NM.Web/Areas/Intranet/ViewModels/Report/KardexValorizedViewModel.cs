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
        public void Fill(CargarDatosContext c, String fechaInicio, String fechaFin, Int32? stockProductId)
        {
            this.StockProductId = stockProductId;
            this.FechaInicio = fechaInicio;
            this.FechaFin = fechaFin;
            if (this.StockProductId.HasValue)
            {
                this.DescriptionInvoice = c.context.StockProduct.FirstOrDefault( x => x.StockProductId == this.StockProductId).Product.InvoiceDescription.ToUpper();
                
                var dtInicio = String.IsNullOrEmpty(this.FechaInicio) ? DateTime.Now.Date : this.FechaInicio.ToDateTime().Date;
                var dtFin = String.IsNullOrEmpty(this.FechaFin) ? DateTime.Now.Date : this.FechaFin.ToDateTime().Date;
                dtFin = dtFin.AddHours(23);

                LstStockProductDetail = c.context.StockProductDetail.Where(x => x.StockProductId == this.StockProductId
               && x.Date >= dtInicio && x.Date <= dtFin).ToList();
            }
        }
    }
}