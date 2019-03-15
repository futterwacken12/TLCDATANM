using CHS.TLC.Data.NM.Web.Areas.Intranet.Models.BE.Json;
using CHS.TLC.Data.NM.Web.Controllers;
using CHS.TLC.Data.NM.Web.Filters;
using CHS.TLC.Data.NM.Web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.Controllers
{
    public class JsonController : BaseController
    {
        // GET: Intranet/Json
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [EncryptedActionParameter]
        public JsonResult GetSupplierInfo(Int32 SupplierId)
        {
            var data = new SupplierInfo();
            try
            {
                var supplier = context.Supplier.FirstOrDefault(x => x.SupplierId == SupplierId);
                data.Code = supplier.Code;
                data.Address = supplier.Address;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetSupplierByName(String q)
        {
            var data = new List<DataSelect2>();
            try
            {
                data = context.Supplier.Where(x => x.BussinessName.Contains(q) &&
                    x.State == ConstantHelpers.ESTADO.ACTIVO).Select(x => new DataSelect2
                    {
                        id = x.SupplierId,
                        text = x.BussinessName
                    }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetProductByCodeAndInvoice(String q, Int32? supplierid)
        {
            var data = new List<DataSelect2>();
            try
            {
                data = context.Product.Where(x => x.SupplierId == supplierid &&
                    (x.InvoiceDescription.Contains(q) || x.InternalCode.Contains(q)) &&
                    x.State == ConstantHelpers.ESTADO.ACTIVO).Select(x => new DataSelect2
                    {
                        id = x.ProductId,
                        text = x.InvoiceDescription.ToUpper()
                    }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetProductById(Int32 ProductId)
        {
            var data = new List<ProductByCodeAndInvoice>();
            try
            {
                data = context.Product.Where(x => x.ProductId == ProductId &&
                    x.State == ConstantHelpers.ESTADO.ACTIVO).Select(x => new ProductByCodeAndInvoice
                    {
                        id = x.ProductId,
                        text = x.InvoiceDescription.ToUpper(),
                        invoiceDescription = x.InvoiceDescription.ToUpper(),
                        code = x.InternalCode
                    }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}