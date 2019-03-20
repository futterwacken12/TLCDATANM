using CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Document;
using CHS.TLC.Data.NM.Web.Controllers;
using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Transactions;
using CHS.TLC.Data.NM.Web.Filters;
using System.IO;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.Controllers
{
    public class DocumentController : BaseController
    {
        // GET: Intranet/Document
        #region PURCHARSE ORDER
        [EncryptedActionParameter]
        public ActionResult ListPO(Int32? Page, String Code, String OrderDate, Int32? SupplierId, Int32? FatherId)
        {
            var model = new LstPOViewModel();
            model.Fill(CargarDatosContext(), Page, Code, OrderDate, SupplierId, FatherId);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult ListPrePO(Int32? Page, String Code, String OrderDate, Int32? SupplierId, Int32? FatherId)
        {
            var model = new ListPrePOViewModel();
            model.Fill(CargarDatosContext(), Page, Code, OrderDate, SupplierId, FatherId);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult AddEditPO(Int32? PurcherseOrderId, Int32? PrePurcherseOrderId, Int32? FatherId)
        {
            var model = new AddEditPOViewModel();
            model.Fill(CargarDatosContext(), PurcherseOrderId, PrePurcherseOrderId, FatherId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditPO(AddEditPOViewModel model, FormCollection frm)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    PurcherseOrder purcharseOrder = null;

                    if (model.PurcherseOrderId.HasValue)
                    {
                        purcharseOrder = context.PurcherseOrder.FirstOrDefault(x => x.PurcherseOrderId == model.PurcherseOrderId);
                    }
                    else
                    {
                        purcharseOrder = new PurcherseOrder();
                        purcharseOrder.State = ConstantHelpers.ESTADO.ACTIVO;
                        context.PurcherseOrder.Add(purcharseOrder);
                    }

                    purcharseOrder.RegistrationDate = model.RegistrationDate.ToDateTime();
                    purcharseOrder.CurrencyId = model.CurrencyId;
                    purcharseOrder.IncotermsId = model.IncotermsId;
                    purcharseOrder.Total = model.Total;
                    purcharseOrder.ToleranceId = model.ToleranceId;
                    purcharseOrder.ToleranceRemark = model.ToleranceRemark;
                    purcharseOrder.ShipmentSample = model.ShipmentSample;
                    purcharseOrder.TreasuryId = model.TreasuryId;
                    purcharseOrder.PortId = model.PortId;
                    purcharseOrder.BeneficiarySupply = model.BeneficiarySupply;
                    purcharseOrder.BeneficiarySupplyAdress = model.BeneficiarySupplyAdress;
                    purcharseOrder.BeneficiaryBankAdress = model.BeneficiaryBankAdress;
                    purcharseOrder.BankAccountnumber = model.BankAccountnumber;
                    purcharseOrder.SwiftCode = model.SwiftCode;
                    purcharseOrder.IntermediaryBank = model.IntermediaryBank;
                    purcharseOrder.SwiftIntermediaryBank = model.SwiftIntermediaryBank;
                    purcharseOrder.SupplierEmail = model.SupplierEmail;
                    purcharseOrder.CEOEmail = model.CEOEmail;
                    purcharseOrder.Code = model.Code ?? String.Empty;
                    purcharseOrder.OrderDate = model.OrderDate.ToDateTime();
                    purcharseOrder.PrePurcherseOrderId = model.PrePurcherseOrderId.Value;                    
                    purcharseOrder.DocumentTypeId = context.DocumentType.FirstOrDefault( x => x.Acronym == ConstantHelpers.TIPODOCUMENTO.PURCHARSE_ORDER).DocumentTypeId;

                    Decimal Total = 0;
                    var lstPrice = frm.AllKeys.Where(x => x.StartsWith("price-")).ToList();
                    foreach (var quantity in lstPrice)
                    {
                        var prePurcherseOrderDetailId = quantity.Replace("price-", String.Empty).ToInteger();

                        PrePurcherseOrderDetail detail = context.PrePurcherseOrderDetail.FirstOrDefault( x => x.PrePurcherseOrderDetailId == prePurcherseOrderDetailId);


                        var price = frm[quantity].ToDecimal();
                        Total += price;                       

                        detail.Price = price;
                    }

                    context.SaveChanges();

                    if (String.IsNullOrEmpty(model.Code))
                    {
                        purcharseOrder.Code = purcharseOrder.PrePurcherseOrderId.ToString();

                        context.SaveChanges();
                    }


                    transaction.Complete();
                }

                PostMessage(MessageType.Success);
                return RedirectToAction("ListPO", new { FatherId = model.FatherId});
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.PurcherseOrderId, model.PrePurcherseOrderId, model.FatherId);
                return View(model);
            }
        }
        [EncryptedActionParameter]
        public ActionResult AddEditPrePO(Int32? PrePurcherseOrderId, Int32? FatherId)
        {
            var model = new AddEditPrePOViewModel();
            model.Fill(CargarDatosContext(), PrePurcherseOrderId, FatherId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditPrePO(AddEditPrePOViewModel model, FormCollection frm)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    PrePurcherseOrder prePO = null;

                    if (model.PrePurcherseOrderId.HasValue)
                    {
                        prePO = context.PrePurcherseOrder.Include(x => x.PrePurcherseOrderDetail).FirstOrDefault(x => x.PrePurcherseOrderId == model.PrePurcherseOrderId);

                        var lstPrePurcherseOrderDetail = prePO.PrePurcherseOrderDetail.ToList();
                        foreach (var d in lstPrePurcherseOrderDetail)
                        {
                            context.PrePurcherseOrderDetail.Remove(d);
                        }
                    }
                    else
                    {
                        prePO = new PrePurcherseOrder();
                        prePO.State = ConstantHelpers.ESTADO.ACTIVO;
                        context.PrePurcherseOrder.Add(prePO);
                    }

                    prePO.RegistrationDate = model.Registration.ToDateTime();
                    prePO.SupplierId = model.SupplierId;
                    prePO.CountryId = model.CountryId;
                    prePO.ShipmentDate = model.ShipmentDate.ToDateTime();
                    prePO.Text = model.Text;
                    prePO.SendSupply = model.SendSupply;
                    prePO.Code = model.Code;
                    prePO.DateOrder = model.DateOrder.ToDateTime();


                    var lstQuantity = frm.AllKeys.Where(x => x.StartsWith("quantity-")).ToList();
                    foreach (var quantity in lstQuantity)
                    {
                        PrePurcherseOrderDetail detail = new PrePurcherseOrderDetail();

                        var productId = quantity.Replace("quantity-", String.Empty).ToInteger();
                        var intQuantity = frm[quantity].ToInteger();
                        var measureunit = frm["measureunit-" + productId].ToInteger();

                        detail.ProductId = productId;
                        detail.Quantity = intQuantity;
                        detail.MeasureUnitId = measureunit;
                        detail.State = ConstantHelpers.ESTADO.ACTIVO;
                        detail.PrePurcherseOrder = prePO;

                        context.PrePurcherseOrderDetail.Add(detail);
                    }

                    context.SaveChanges();

                    if (String.IsNullOrEmpty(model.Code))
                    {
                        prePO.Code = prePO.PrePurcherseOrderId.ToString();
                        context.SaveChanges();
                    }

                    transaction.Complete();
                }

                PostMessage(MessageType.Success);
                return RedirectToAction("ListPrePO",new { FatherId = model.FatherId });
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.PrePurcherseOrderId, model.FatherId);
                return View(model);
            }
        }
        [EncryptedActionParameter]
        public ActionResult _DeletePO(Int32 PurcherseOrderId)
        {
            var model = new _DeletePOViewModel();
            model.Fill(CargarDatosContext(), PurcherseOrderId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _DeletePO(_DeletePOViewModel model)
        {
            try
            {
                PurcherseOrder purcherseOrder = null;

                if (model.PurcherseOrderId > 0)
                {
                    purcherseOrder = context.PurcherseOrder.FirstOrDefault(x => x.PurcherseOrderId == model.PurcherseOrderId);
                    purcherseOrder.State = ConstantHelpers.ESTADO.INACTIVO;
                }

                context.SaveChanges();
                PostMessage(MessageType.Success);
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
            }

            return RedirectToAction("ListPO");
        }
        [EncryptedActionParameter]
        public ActionResult _DeletePrePO(Int32 PrePurcherseOrderId)
        {
            var model = new _DeletePrePOViewModel();
            model.Fill(CargarDatosContext(), PrePurcherseOrderId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _DeletePrePO(_DeletePrePOViewModel model)
        {
            try
            {
                PrePurcherseOrder prePurcherseOrder = null;

                if (model.PrePurcherseOrderId > 0)
                {
                    prePurcherseOrder = context.PrePurcherseOrder.FirstOrDefault(x => x.PrePurcherseOrderId == model.PrePurcherseOrderId);
                    prePurcherseOrder.State = ConstantHelpers.ESTADO.INACTIVO;
                }

                context.SaveChanges();
                PostMessage(MessageType.Success);
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
            }

            return RedirectToAction("ListPrePO");
        }
        [EncryptedActionParameter]
        public ActionResult _AddEditDocumentPrePO(Int32 PrePurcherseOrderId)
        {
            var model = new _AddEditDocumentPrePOViewModel();
            model.Fill(CargarDatosContext(), PrePurcherseOrderId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _AddEditDocumentPrePO(_AddEditDocumentPrePOViewModel model)
        {
            try
            {
                PrePurcherseOrder prePurcherseOrder = context.PrePurcherseOrder.FirstOrDefault(x => x.PrePurcherseOrderId == model.PrePurcherseOrderId);
                if (prePurcherseOrder != null)
                {
                    byte[] data;
                    using (Stream inputStream = model.File.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }

                        data = memoryStream.ToArray();
                    }

                    prePurcherseOrder.DocumentContentType = model.File.ContentType;
                    prePurcherseOrder.DocumentName = model.DocumentName;
                    prePurcherseOrder.DocumentPath = data;
                    prePurcherseOrder.DocumentExtension = Path.GetExtension(model.File.FileName);

                    context.SaveChanges();
                    PostMessage(MessageType.Success);
                }
                else
                {
                    PostMessage(MessageType.Error);
                }

            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
            }

            return RedirectToAction("ListPrePO");
        }
        [EncryptedActionParameter]
        public FileContentResult DownloadDocumentPrePO(Int32 PrePurcherseOrderId)
        {
            try
            {
                var prePO = context.PrePurcherseOrder.FirstOrDefault(x => x.PrePurcherseOrderId == PrePurcherseOrderId);

                if (prePO != null)
                {
                    byte[] data = prePO.DocumentPath;
                    var extension = prePO.DocumentExtension;
                    var contentType = prePO.DocumentContentType;
                    var documentName = prePO.DocumentName;

                    return File(data, contentType, documentName + extension);
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}