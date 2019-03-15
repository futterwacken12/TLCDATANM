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

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.Controllers
{
    public class DocumentController : BaseController
    {
        // GET: Intranet/Document
        public ActionResult Index()
        {
            return View();
        }
        [EncryptedActionParameter]
        public ActionResult ListPrePO(Int32? Page, String Code, String OrderDate, Int32? SupplierId)
        {
            var model = new ListPrePOViewModel();
            model.Fill(CargarDatosContext(), Page, Code, OrderDate, SupplierId);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult AddEditPrePO(Int32? PrePurcherseOrderId)
        {
            var model = new AddEditPrePOViewModel();
            model.Fill(CargarDatosContext(), PrePurcherseOrderId);
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
                    }

                    context.SaveChanges();

                    transaction.Complete();
                }                

                PostMessage(MessageType.Success);
                return RedirectToAction("ListPrePO");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.PrePurcherseOrderId);
                return View(model);
            }
        }
    }
}