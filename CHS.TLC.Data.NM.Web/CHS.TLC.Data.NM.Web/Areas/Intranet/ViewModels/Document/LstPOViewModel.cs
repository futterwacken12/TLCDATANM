using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Document
{
    public class LstPOViewModel
    {
        public String Code { get; set; }
        public Int32? FatherId { get; set; }
        public String OrderDate { get; set; }
        public Int32? SupplierId { get; set; }
        public List<Supplier> LstSupplier { get; set; } = new List<Supplier>();
        public Int32? Page { get; set; }
        public IPagedList<PurcherseOrder> LstPO { get; set; }
        public void Fill(CargarDatosContext c, Int32? page, String code, String orderDate, Int32? supplierId, Int32? fatherId)
        {
            this.Page = page ?? 1;
            this.FatherId = fatherId;
            this.Code = code;
            this.OrderDate = orderDate;
            this.SupplierId = supplierId;

            LstSupplier = c.context.Supplier.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();

            var query = c.context.PurcherseOrder.Where( x => x.State == ConstantHelpers.ESTADO.ACTIVO).AsQueryable();
            if (!String.IsNullOrEmpty(this.Code))
            {
                query = query.Where(x => x.Code.Contains(this.Code));
            }
            if (!String.IsNullOrEmpty(this.OrderDate))
            {
                var dtOrderDate = this.OrderDate.ToDateTime();
                query = query.Where(x => x.OrderDate.Year == dtOrderDate.Year &&
               x.OrderDate.Month == dtOrderDate.Month &&
               x.OrderDate.Day == dtOrderDate.Day);
            }
            if (this.SupplierId.HasValue)
            {
                query = query.Where(x => x.PrePurcherseOrder.SupplierId == this.SupplierId);
            }

            LstPO = query.OrderByDescending(x => x.PurcherseOrderId).ToPagedList(this.Page.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}