using CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Store;
using CHS.TLC.Data.NM.Web.Controllers;
using CHS.TLC.Data.NM.Web.Filters;
using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.Controllers
{
    public class StoreController : BaseController
    {
        // GET: Intranet/Store
        public ActionResult Index()
        {
            return View();
        }
        [EncryptedActionParameter]
        public ActionResult ListOutputNote(Int32? Page, String State, String Date, Int32? MovementTypeId, String Code, Int32? FatherId)
        {
            var model = new LstOuputNoteViewModel();
            model.Fill(CargarDatosContext(), Page, State, Date, MovementTypeId, Code, FatherId);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult AddEditOutputNote(Int32? OutputNoteId)
        {
            var model = new AddEditOutputNoteViewModel();
            model.Fill(CargarDatosContext(), OutputNoteId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditOutputNote(AddEditOutputNoteViewModel model, FormCollection frm)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    OutputNote outputNote = null;

                    if (model.OutputNoteId.HasValue)
                    {
                        PostMessage(MessageType.Warning, "No se puede editar una nota de salida.");
                        return RedirectToAction("ListOutputNote");
                        //entryNote = context.EntryNote.FirstOrDefault(x => x.EntryNoteId == model.EntryNoteId);
                    }
                    else
                    {
                        outputNote = new OutputNote();
                        outputNote.State = ConstantHelpers.ESTADO.ACTIVO;
                        outputNote.Date = DateTime.Now;
                        outputNote.Time = DateTime.Now.TimeOfDay;
                        context.OutputNote.Add(outputNote);
                    }

                    outputNote.MovementTypeId = model.MovementTypeId;
                    outputNote.OriginalStoreId = model.OriginalStoreId;
                    outputNote.DestinationStoreId = model.DestinationStoreId;
                    if(model.DocumentTypeId.HasValue)
                        outputNote.DocumentCode = context.DocumentType.FirstOrDefault( x => x.DocumentTypeId == model.DocumentTypeId).Acronym;
                    outputNote.DocumentId = model.DocumentId;
                    outputNote.Code = model.Code ?? String.Empty;

                    List<StockProduct> lstStockProduct = new List<StockProduct>();
                    var lstQuantity = frm.AllKeys.Where(x => x.StartsWith("qreal-")).ToList();

                    List<StockProduct> lstStock = new List<StockProduct>();
                    foreach (var q in lstQuantity)
                    {
                        var data = q.Replace("qreal-", String.Empty).Split('-');
                        var productId = data[1].ToInteger();

                        lstStock = context.StockProduct.Where(x => x.ProductId == productId && x.StoreId == model.OriginalStoreId && x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();
                        foreach (var stock in lstStock)
                        {
                            if (stock.Quantity <= 0)
                            {
                                PostMessage(MessageType.Warning, "Se canceló la orden debido a que no se encuentra stock de: " + stock.Product.InvoiceDescription);
                                return RedirectToAction("ListOutputNote");
                            }
                        }
                    }

                    foreach (var q in lstQuantity)
                    {
                        var data = q.Replace("qreal-", String.Empty).Split('-');
                        var productId = data[1].ToInteger();
                        var index = data[0].ToInteger();

                        var stockProductDetail = new StockProductDetail();
                        context.StockProductDetail.Add(stockProductDetail);
                        stockProductDetail.OutputNote = outputNote;
                        stockProductDetail.State = ConstantHelpers.ESTADO.ACTIVO;
                        stockProductDetail.Operation = ConstantHelpers.OPERATION.OUTPUT;
                        stockProductDetail.Value = frm["qreal-" + index + "-" + productId].ToDecimal();
                        stockProductDetail.Date = DateTime.Now;
                        
                        var stock = lstStock.FirstOrDefault(x => x.ProductId == productId && x.StoreId == model.OriginalStoreId && x.State == ConstantHelpers.ESTADO.ACTIVO);
                        if (stock != null)
                        {
                            stock.Quantity -= stockProductDetail.Value;
                        }
                        else
                        {
                            stock = lstStockProduct.FirstOrDefault(x => x.ProductId == productId && x.StoreId == model.OriginalStoreId && x.State == ConstantHelpers.ESTADO.ACTIVO);
                            if (stock != null)
                            {
                                stock.Quantity -= stockProductDetail.Value;
                            }
                        }

                        stockProductDetail.StockProduct = stock;



                        OutputNoteDetail outputNoteDetail = new OutputNoteDetail();
                        outputNoteDetail.OutputNote = outputNote;
                        outputNoteDetail.State = ConstantHelpers.ESTADO.ACTIVO;
                        outputNoteDetail.TypePayment = (frm["typepayment-" + index + "-" + productId] ?? "TOT").ToString();
                        outputNoteDetail.RealQuantity = frm["qreal-" + index + "-" + productId].ToDecimal();
                        outputNoteDetail.RealMeasureUnit = "m";
                        outputNoteDetail.ProductId = productId;
                        outputNoteDetail.SecRealMeasureUnit = "rollos";
                        outputNoteDetail.PStockQuantity = frm["pstockquantity-" + index + "-" + productId].ToDecimal();
                        outputNoteDetail.SStockQuantity = frm["sstockquantity-" + index + "-" + productId].ToDecimal();
                        outputNoteDetail.SecRealQuantity = frm["qsreal-" + index + "-" + productId].ToDecimal();
                        context.OutputNoteDetail.Add(outputNoteDetail);
                    }

                    //context.StockProduct.AddRange(lstStockProduct);
                    context.SaveChanges();

                    if (String.IsNullOrEmpty(model.Code))
                    {
                        outputNote.Code = outputNote.OutputNoteId.ToString();
                        context.SaveChanges();
                    }

                    transaction.Complete();
                }


                PostMessage(MessageType.Success);
                return RedirectToAction("ListOutputNote");

            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.OutputNoteId);
                return View(model);
            }
        }
        [EncryptedActionParameter]
        public ActionResult ListEntryNote(Int32? Page, String State, String Date, Int32? MovementTypeId, String Code, Int32? FatherId)
        {
            var model = new LstEntryNoteViewModel();
            model.Fill(CargarDatosContext(), Page, State, Date, MovementTypeId, Code, FatherId);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult AddEditEntryNote(Int32? EntryNoteId)
        {
            var model = new AddEditEntryNoteViewModel();
            model.Fill(CargarDatosContext(), EntryNoteId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditEntryNote(AddEditEntryNoteViewModel model, FormCollection frm)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    EntryNote entryNote = null;

                    if (model.EntryNoteId.HasValue)
                    {
                        PostMessage(MessageType.Warning,"No se puede editar una nota de ingreso.");
                        return RedirectToAction("ListEntryNote");
                        //entryNote = context.EntryNote.FirstOrDefault(x => x.EntryNoteId == model.EntryNoteId);
                    }
                    else
                    {
                        entryNote = new EntryNote();
                        entryNote.State = ConstantHelpers.ESTADO.ACTIVO;
                        entryNote.Date = DateTime.Now;
                        entryNote.Time = DateTime.Now.TimeOfDay;
                        context.EntryNote.Add(entryNote);
                    }

                    entryNote.MovementTypeId = model.MovementTypeId;
                    entryNote.SupplierGuideNumber = model.SupplierGuideNumber;
                    entryNote.TransportGuideNumber = model.TransportGuideNumber;
                    entryNote.Seal = model.Seal;
                    entryNote.TransportTime = model.TransportTime;
                    entryNote.DestinationStoreId = model.DestinationStoreId;
                    entryNote.SupplierId = model.SupplierId;
                    entryNote.DocumentCode = model.DocumentCode;
                    entryNote.DocumentId = model.DocumentId;
                    entryNote.Code = model.Code ?? String.Empty;

                    List<StockProduct> lstStockProduct = new List<StockProduct>();
                    var lstQuantity = frm.AllKeys.Where(x => x.StartsWith("qreal-")).ToList();
                    foreach (var q in lstQuantity)
                    {
                        var data = q.Replace("qreal-", String.Empty).Split('-');
                        var PrePurcherseOrderDetailId = data[1].ToInteger();
                        var index = data[0].ToInteger();

                        PrePurcherseOrderDetail detail = context.PrePurcherseOrderDetail.FirstOrDefault(x => x.PrePurcherseOrderDetailId == PrePurcherseOrderDetailId);

                        var stockProductDetail = new StockProductDetail();
                        context.StockProductDetail.Add(stockProductDetail);
                        stockProductDetail.EntryNote = entryNote;
                        stockProductDetail.State = ConstantHelpers.ESTADO.ACTIVO;
                        stockProductDetail.Operation = ConstantHelpers.OPERATION.ENTRY;
                        stockProductDetail.Value = frm["qreal-" + index + "-" + PrePurcherseOrderDetailId].ToDecimal();
                        stockProductDetail.Date = DateTime.Now;
                        stockProductDetail.CUPrice = detail.Price;
                        stockProductDetail.CurrencyId = detail.PrePurcherseOrder.PurcherseOrder.FirstOrDefault(x => x.State == ConstantHelpers.ESTADO.ACTIVO).CurrencyId;

                        var stock = context.StockProduct.FirstOrDefault(x => x.ProductId == detail.ProductId && x.StoreId == model.DestinationStoreId && x.State == ConstantHelpers.ESTADO.ACTIVO);
                        if (stock != null)
                        {
                            stock.Quantity += stockProductDetail.Value;
                        }
                        else
                        {
                            stock = lstStockProduct.FirstOrDefault(x => x.ProductId == detail.ProductId && x.StoreId == model.DestinationStoreId && x.State == ConstantHelpers.ESTADO.ACTIVO);
                            if (stock != null)
                            {
                                stock.Quantity += stockProductDetail.Value;
                            }
                            else
                            {
                                stock = new StockProduct();
                                stock.Quantity = stockProductDetail.Value;
                                stock.State = ConstantHelpers.ESTADO.ACTIVO;
                                stock.ProductId = detail.ProductId;
                                stock.StoreId = model.DestinationStoreId;
                                lstStockProduct.Add(stock);
                            }
                            //context.StockProduct.Add(stock);
                        }

                        stockProductDetail.StockProduct = stock;



                        EntryNoteDetail entryNoteDetail = new EntryNoteDetail();
                        entryNoteDetail.EntryNote = entryNote;
                        entryNoteDetail.State = ConstantHelpers.ESTADO.ACTIVO;
                        entryNoteDetail.TypePayment = (frm["typepayment-" + index + "-" + PrePurcherseOrderDetailId] ?? "TOT").ToString();
                        entryNoteDetail.RealQuantity = frm["qreal-" + index + "-" + PrePurcherseOrderDetailId].ToDecimal();
                        entryNoteDetail.RealMeasureUnit = "m";
                        entryNoteDetail.PrePurcherseOrderDetailId = PrePurcherseOrderDetailId;
                        entryNoteDetail.SecRealMeasureUnit = "rollos";
                        entryNoteDetail.ProductId = detail.ProductId;
                        entryNoteDetail.SecRealQuantity = frm["qsreal-" + index + "-" + PrePurcherseOrderDetailId].ToDecimal();
                        context.EntryNoteDetail.Add(entryNoteDetail);
                    }

                    context.StockProduct.AddRange(lstStockProduct);
                    context.SaveChanges();

                    if (String.IsNullOrEmpty(model.Code))
                    {
                        entryNote.Code = entryNote.EntryNoteId.ToString();
                        context.SaveChanges();
                    }

                    transaction.Complete();
                }


                PostMessage(MessageType.Success);
                return RedirectToAction("ListEntryNote");

            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.EntryNoteId);
                return View(model);
            }
        }

        [EncryptedActionParameter]
        public ActionResult _DeleteEntryNote(Int32 EntryNoteId)
        {
            var model = new _DeleteEntryNoteViewModel();
            model.Fill(CargarDatosContext(), EntryNoteId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteEntryNote(_DeleteEntryNoteViewModel model)
        {
            try
            {
                EntryNote entryNote = null;

                if (model.EntryNoteId > 0)
                {
                    entryNote = context.EntryNote
                        .FirstOrDefault(x => x.EntryNoteId == model.EntryNoteId);
                    entryNote.State = ConstantHelpers.ESTADO.INACTIVO;

                    var lstEntryNoteDetail = context.EntryNoteDetail.Where(x => x.EntryNoteId == entryNote.EntryNoteId).ToList();
                    lstEntryNoteDetail.ForEach(x => x.State = ConstantHelpers.ESTADO.INACTIVO);

                    var lstStockProductDetail = context.StockProductDetail.Where(x => x.EntryNoteId == entryNote.EntryNoteId).ToList();
                    lstStockProductDetail.ForEach(x => x.State = ConstantHelpers.ESTADO.INACTIVO);
                        
                    foreach (var item in lstStockProductDetail)
                    {
                        var stock = context.StockProduct.FirstOrDefault( x => x.StockProductId == item.StockProductId);
                        stock.Quantity -= item.Value;
                    }
                }

                context.SaveChanges();
                PostMessage(MessageType.Success);
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
            }

            return RedirectToAction("ListEntryNote");
        }
    }
}