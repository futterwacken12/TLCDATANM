﻿using CHS.TLC.Data.NM.Web.Areas.Intranet.Models.BE.Json;
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
        public JsonResult GetPrePurcherseOrderByCode(String q)
        {
            var data = new List<DataSelect2>();
            try
            {
                data = context.PrePurcherseOrder.Where(x => x.Code.Contains(q) &&
                    x.State == ConstantHelpers.ESTADO.ACTIVO).Select(x => new DataSelect2
                    {
                        id = x.PrePurcherseOrderId,
                        text = x.Code
                    }).ToList();
                
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetPurcherseOrderInfo(Int32 PurcherseOrderId)
        {
            var data = new List<PurcherseOrderInfo>();
            try
            {
                var documentCode = context.PurcherseOrder.FirstOrDefault( x => x.PurcherseOrderId == PurcherseOrderId).Code;
                var prePurcherseOrderId = context.PurcherseOrder.FirstOrDefault(x => x.PurcherseOrderId == PurcherseOrderId).PrePurcherseOrderId;
                data = context.PrePurcherseOrderDetail.Where(x => x.PrePurcherseOrderId == prePurcherseOrderId).Select( x => new PurcherseOrderInfo
                {
                    descriptionLocal = x.Product.LocalDescription,
                    descriptionInvoice = x.Product.InvoiceDescription,
                    code = x.Product.InternalCode,
                    family = x.Product.SubFamily.Family.Description,
                    design = "564",
                    quantity = x.Quantity,
                    unit = x.Product.MeasureUnit.Acronym,
                    prePurcherseOrderDetailId = x.PrePurcherseOrderDetailId,
                    supplierName = x.PrePurcherseOrder.Supplier.BussinessName,
                    supplierId = x.PrePurcherseOrder.SupplierId,
                    documentCode = documentCode
                }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetPrePurcherseOrderInfo(Int32 PrePurcherseOrderId)
        {
            var data = new PrePurcherseOrderInfo();
            try
            {
                var prePo = context.PrePurcherseOrder.FirstOrDefault(x => x.PrePurcherseOrderId == PrePurcherseOrderId);

                data.orderDate = prePo.DateOrder.ToString("dd/MM/yyyy");
                data.supplierName = prePo.Supplier.BussinessName;
                data.supplierAddress = prePo.Supplier.Address;
                data.countryId = prePo.Supplier.CountryId.Value;
                data.supplierId = prePo.SupplierId;
                data.shipmentDate = prePo.ShipmentDate.ToString("dd/MM/yyyy");

                data.lstDetail = prePo.PrePurcherseOrderDetail.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).Select(x => new PrePurcherseOrderDetailInfo
                {
                    quantity = x.Quantity,
                    descriptionInvoice = x.Product.InvoiceDescription.ToUpper(),
                    productCode = x.Product.InternalCode,
                    prePurcherseOrderDetailId = x.PrePurcherseOrderDetailId

                }).ToList();


                var contact = prePo.Supplier.Contact.FirstOrDefault( x => x.State == ConstantHelpers.ESTADO.ACTIVO);
                data.supplierEmail = contact == null ? "" : contact.Email;

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
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
        public JsonResult GetDocumentByCode(String q, Int32 documentTypeId)
        {
            var data = new List<DataSelect2>();
            try
            {
                var acronym = context.DocumentType.FirstOrDefault(x => x.DocumentTypeId == documentTypeId).Acronym;

                switch (acronym)
                {
                    case "PO":
                        data = context.PurcherseOrder.Where(x => x.Code.Contains(q) &&
                                x.State == ConstantHelpers.ESTADO.ACTIVO).Select(x => new DataSelect2
                                {
                                    id = x.PurcherseOrderId,
                                    text = x.Code
                                }).ToList();
                        break;
                    default:
                        break;
                }               

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
        public JsonResult GetProductByCodeAndInvoicePrePO(String q)
        {
            var data = new List<DataSelect2>();
            try
            {
                data = context.PrePurcherseOrderDetail.Where(x =>
                    (x.Product.InvoiceDescription.Contains(q))&&
                    x.State == ConstantHelpers.ESTADO.ACTIVO).Select(x => new DataSelect2
                    {
                        id = x.ProductId,
                        text = x.Product.InvoiceDescription.ToUpper()
                    }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetProductByCodeAndInvoice(String q)
        {
            var data = new List<DataSelect2>();
            try
            {
                data = context.Product.Where(x =>
                    (x.InvoiceDescription.Contains(q)) &&
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

        [HttpGet]
        public JsonResult GetProductPrePOById(Int32 ProductId)
        {
            var data = new List<ProductByCodeAndInvoice>();
            try
            {
                data = context.PrePurcherseOrderDetail.Where(x => x.ProductId == ProductId &&
                    x.State == ConstantHelpers.ESTADO.ACTIVO).Select(x => new ProductByCodeAndInvoice
                    {
                        id = x.PrePurcherseOrderDetailId,
                        text = x.Product.InvoiceDescription.ToUpper(),
                        invoiceDescription = x.Product.InvoiceDescription.ToUpper(),
                        code = x.Product.InternalCode,
                        measureUnit = x.MeasureUnit.Acronym,
                        quantity = x.Quantity
                    }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult GetSubfamilyByFamily(Int32 FamilyId)
        {
            var LstData = context.SubFamily.Where(x => x.FamilyId == FamilyId && x.State.Equals(ConstantHelpers.ESTADO.ACTIVO))
                .Select(x => new { SubFamilyId = x.SubFamilyId, Description = x.Description }).ToList();
            return Json(LstData);
        }
        [HttpGet]
        public JsonResult GetSKUCodeByFamily(Int32 FamilyId)
        {
            var data = String.Empty;
            try
            {
                var family = context.Family.FirstOrDefault(x => x.FamilyId == FamilyId);
                data = family.SKUCode;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}