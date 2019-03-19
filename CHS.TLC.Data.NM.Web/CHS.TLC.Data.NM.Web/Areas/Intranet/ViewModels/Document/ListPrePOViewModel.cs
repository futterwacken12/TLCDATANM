using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Document
{
    public class ListPrePOViewModel
    {
        public Int32? FatherId { get; set; }
        public String Code { get; set; }
        public String OrderDate { get; set; }
        public Int32? SupplierId { get; set; }
        public List<Supplier> LstSupplier { get; set; } = new List<Supplier>();
        public Int32? Page { get; set; }
        public IPagedList<PrePurcherseOrder> LstPrePO { get; set; }
        public void Fill(CargarDatosContext c, Int32? page, String code, String orderDate, Int32? supplierId, Int32? fatherId)
        {
            this.FatherId = fatherId;
            this.Page = page ?? 1;
            this.Code = code;
            this.OrderDate = orderDate;
            this.SupplierId = supplierId;

            LstSupplier = c.context.Supplier.Where( x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();

            var query = c.context.PrePurcherseOrder.AsQueryable();
            if (!String.IsNullOrEmpty(this.Code))
            {
                query = query.Where(x => x.Code.Contains(this.Code));
            }
            if (!String.IsNullOrEmpty(this.OrderDate))
            {
                var dtOrderDate = this.OrderDate.ToDateTime();
                query = query.Where( x => x.DateOrder.Year == dtOrderDate.Year &&
                x.DateOrder.Month == dtOrderDate.Month &&
                x.DateOrder.Day == dtOrderDate.Day);
            }
            if (this.SupplierId.HasValue)
            {
                query = query.Where( x => x.SupplierId == this.SupplierId);
            }

            LstPrePO = query.OrderByDescending(x => x.PrePurcherseOrderId).ToPagedList(this.Page.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}