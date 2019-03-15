using CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master;
using CHS.TLC.Data.NM.Web.Controllers;
using CHS.TLC.Data.NM.Web.Filters;
using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.Controllers
{
    public class MasterController : BaseController
    {
        // GET: Intranet/Master
        #region Family
        public ActionResult ListFamily(Int32? Page, String Description)
        {
            var model = new ListFamilyViewModel();
            model.Fill(CargarDatosContext(), Page, Description);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult AddEditFamily(Int32? FamilyId)
        {
            var model = new AddEditFamilyViewModel();
            model.Fill(CargarDatosContext(), FamilyId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditFamily(AddEditFamilyViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Fill(CargarDatosContext(), model.FamilyId);
                    TryUpdateModel(model);
                    PostMessage(MessageType.Error);
                    return View(model);
                }

                Family family = null;

                if (model.FamilyId.HasValue)
                {
                    family = context.Family.FirstOrDefault(x => x.FamilyId == model.FamilyId);
                }
                else
                {
                    family = new Family();
                    family.State = ConstantHelpers.ESTADO.ACTIVO;
                    context.Family.Add(family);
                }

                family.Description = model.Description;
                family.SKUCode = model.SKUCode;

                context.SaveChanges();
                PostMessage(MessageType.Success);

                return RedirectToAction("ListFamily");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.FamilyId);

                return View(model);
            }
        }
        [EncryptedActionParameter]
        public ActionResult _DeleteFamily(Int32? FamilyId)
        {
            var model = new _DeleteFamilyViewModel();
            model.Fill(CargarDatosContext(), FamilyId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteFamily(_DeleteFamilyViewModel model)
        {
            Family family = null;

            if (model.FamilyId.HasValue)
            {
                family = context.Family.FirstOrDefault(x => x.FamilyId == model.FamilyId);
                family.State = ConstantHelpers.ESTADO.INACTIVO;
            }

            context.SaveChanges();
            PostMessage(MessageType.Success);

            return RedirectToAction("ListFamily");
        }
        #endregion
        #region Bank
        public ActionResult ListBank(Int32? Page, String BankType)
        {
            var model = new ListBankViewModel();
            model.Fill(CargarDatosContext(), Page, BankType);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult AddEditBank(Int32? BankId)
        {
            var model = new AddEditBankViewModel();
            model.Fill(CargarDatosContext(), BankId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditBank(AddEditBankViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Fill(CargarDatosContext(), model.BankId);
                    TryUpdateModel(model);
                    PostMessage(MessageType.Error);
                    return View(model);
                }

                Bank bank = null;

                if (model.BankId.HasValue)
                {
                    bank = context.Bank.FirstOrDefault(x => x.BankId == model.BankId);
                }
                else
                {
                    bank = new Bank();
                    bank.State = ConstantHelpers.ESTADO.ACTIVO;
                    context.Bank.Add(bank);
                }

                bank.Description = model.Description;
                bank.Type = model.Type;

                context.SaveChanges();
                PostMessage(MessageType.Success);

                return RedirectToAction("ListBank");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.BankId);

                return View(model);
            }
        }
        [EncryptedActionParameter]
        public ActionResult _DeleteBank(Int32? BankId)
        {
            var model = new _DeleteBankViewModel();
            model.Fill(CargarDatosContext(), BankId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteBank(_DeleteBankViewModel model)
        {
            Bank bank = null;

            if (model.BankId.HasValue)
            {
                bank = context.Bank.FirstOrDefault(x => x.BankId == model.BankId);
                bank.State = ConstantHelpers.ESTADO.INACTIVO;
            }

            context.SaveChanges();
            PostMessage(MessageType.Success);

            return RedirectToAction("ListBank");
        }
        #endregion
        #region MeasureUnit
        public ActionResult ListMeasureUnit(Int32? Page, String Description)
        {
            var model = new ListMeasureUnitViewModel();
            model.Fill(CargarDatosContext(), Page, Description);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult AddEditMeasureUnit(Int32? MeasureUnitId)
        {
            var model = new AddEditMeasureUnitViewModel();
            model.Fill(CargarDatosContext(), MeasureUnitId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditMeasureUnit(AddEditMeasureUnitViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Fill(CargarDatosContext(), model.MeasureUnitId);
                    TryUpdateModel(model);
                    PostMessage(MessageType.Error);
                    return View(model);
                }

                MeasureUnit measureUnit = null;

                if (model.MeasureUnitId.HasValue)
                {
                    measureUnit = context.MeasureUnit.FirstOrDefault(x => x.MeasureUnitId == model.MeasureUnitId);
                }
                else
                {
                    measureUnit = new MeasureUnit();
                    measureUnit.State = ConstantHelpers.ESTADO.ACTIVO;
                    context.MeasureUnit.Add(measureUnit);
                }

                measureUnit.Description = model.Description;
                measureUnit.Acronym = model.Acronym;

                context.SaveChanges();
                PostMessage(MessageType.Success);

                return RedirectToAction("ListMeasureUnit");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.MeasureUnitId);

                return View(model);
            }
        }
        [EncryptedActionParameter]
        public ActionResult _DeleteMeasureUnit(Int32? MeasureUnitId)
        {
            var model = new _DeleteMeasureUnitViewModel();
            model.Fill(CargarDatosContext(), MeasureUnitId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteMeasureUnit(_DeleteMeasureUnitViewModel model)
        {
            MeasureUnit MeasureUnit = null;

            if (model.MeasureUnitId.HasValue)
            {
                MeasureUnit = context.MeasureUnit.FirstOrDefault(x => x.MeasureUnitId == model.MeasureUnitId);
                MeasureUnit.State = ConstantHelpers.ESTADO.INACTIVO;
            }

            context.SaveChanges();
            PostMessage(MessageType.Success);

            return RedirectToAction("ListMeasureUnit");
        }
        #endregion
        #region SubFamily
        public ActionResult ListSubFamily(Int32? Page, String Description)
        {
            var model = new ListSubFamilyViewModel();
            model.Fill(CargarDatosContext(), Page, Description);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult AddEditSubFamily(Int32? SubFamilyId)
        {
            var model = new AddEditSubFamilyViewModel();
            model.Fill(CargarDatosContext(), SubFamilyId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditSubFamily(AddEditSubFamilyViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Fill(CargarDatosContext(), model.SubFamilyId);
                    TryUpdateModel(model);
                    PostMessage(MessageType.Error);
                    return View(model);
                }

                SubFamily subFamily = null;

                if (model.SubFamilyId.HasValue)
                {
                    subFamily = context.SubFamily.FirstOrDefault(x => x.SubFamilyId == model.SubFamilyId);
                }
                else
                {
                    subFamily = new SubFamily();
                    subFamily.State = ConstantHelpers.ESTADO.ACTIVO;
                    context.SubFamily.Add(subFamily);
                }

                subFamily.Description = model.Description;
                subFamily.FamilyId = model.FamilyId;

                context.SaveChanges();
                PostMessage(MessageType.Success);

                return RedirectToAction("ListSubFamily");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.SubFamilyId);

                return View(model);
            }
        }
        [EncryptedActionParameter]
        public ActionResult _DeleteSubFamily(Int32? SubFamilyId)
        {
            var model = new _DeleteSubFamilyViewModel();
            model.Fill(CargarDatosContext(), SubFamilyId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteSubFamily(_DeleteSubFamilyViewModel model)
        {
            SubFamily SubFamily = null;

            if (model.SubFamilyId.HasValue)
            {
                SubFamily = context.SubFamily.FirstOrDefault(x => x.SubFamilyId == model.SubFamilyId);
                SubFamily.State = ConstantHelpers.ESTADO.INACTIVO;
            }

            context.SaveChanges();
            PostMessage(MessageType.Success);

            return RedirectToAction("ListSubFamily");
        }
        #endregion
        #region TaxPercentage
        public ActionResult ListTaxPercentage(Int32? Page, String Description)
        {
            var model = new ListTaxPercentageViewModel();
            model.Fill(CargarDatosContext(), Page, Description);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult AddEditTaxPercentage(Int32? TaxPercentageId)
        {
            var model = new AddEditTaxPercentageViewModel();
            model.Fill(CargarDatosContext(), TaxPercentageId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditTaxPercentage(AddEditTaxPercentageViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Fill(CargarDatosContext(), model.TaxPercentageId);
                    TryUpdateModel(model);
                    PostMessage(MessageType.Error);
                    return View(model);
                }

                TaxPercentage taxPercentage = null;

                if (model.TaxPercentageId.HasValue)
                {
                    taxPercentage = context.TaxPercentage.FirstOrDefault(x => x.TaxPercentageId == model.TaxPercentageId);
                }
                else
                {
                    taxPercentage = new TaxPercentage();
                    taxPercentage.State = ConstantHelpers.ESTADO.ACTIVO;
                    context.TaxPercentage.Add(taxPercentage);
                }

                taxPercentage.Description = model.Description;

                context.SaveChanges();
                PostMessage(MessageType.Success);

                return RedirectToAction("ListTaxPercentage");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.TaxPercentageId);

                return View(model);
            }
        }
        [EncryptedActionParameter]
        public ActionResult _DeleteTaxPercentage(Int32? TaxPercentageId)
        {
            var model = new _DeleteTaxPercentageViewModel();
            model.Fill(CargarDatosContext(), TaxPercentageId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteTaxPercentage(_DeleteTaxPercentageViewModel model)
        {
            TaxPercentage TaxPercentage = null;

            if (model.TaxPercentageId.HasValue)
            {
                TaxPercentage = context.TaxPercentage.FirstOrDefault(x => x.TaxPercentageId == model.TaxPercentageId);
                TaxPercentage.State = ConstantHelpers.ESTADO.INACTIVO;
            }

            context.SaveChanges();
            PostMessage(MessageType.Success);

            return RedirectToAction("ListTaxPercentage");
        }
        #endregion
        #region TypeExistence
        public ActionResult ListTypeExistence(Int32? Page, String Description)
        {
            var model = new ListTypeExistenceViewModel();
            model.Fill(CargarDatosContext(), Page, Description);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult AddEditTypeExistence(Int32? TypeExistenceId)
        {
            var model = new AddEditTypeExistenceViewModel();
            model.Fill(CargarDatosContext(), TypeExistenceId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditTypeExistence(AddEditTypeExistenceViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Fill(CargarDatosContext(), model.TypeExistenceId);
                    TryUpdateModel(model);
                    PostMessage(MessageType.Error);
                    return View(model);
                }

                TypeExistence typeExistence = null;

                if (model.TypeExistenceId.HasValue)
                {
                    typeExistence = context.TypeExistence.FirstOrDefault(x => x.TypeExistenceId == model.TypeExistenceId);
                }
                else
                {
                    typeExistence = new TypeExistence();
                    typeExistence.State = ConstantHelpers.ESTADO.ACTIVO;
                    context.TypeExistence.Add(typeExistence);
                }

                typeExistence.Description = model.Description;

                context.SaveChanges();
                PostMessage(MessageType.Success);

                return RedirectToAction("ListTypeExistence");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.TypeExistenceId);

                return View(model);
            }
        }
        [EncryptedActionParameter]
        public ActionResult _DeleteTypeExistence(Int32? TypeExistenceId)
        {
            var model = new _DeleteTypeExistenceViewModel();
            model.Fill(CargarDatosContext(), TypeExistenceId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteTypeExistence(_DeleteTypeExistenceViewModel model)
        {
            TypeExistence TypeExistence = null;

            if (model.TypeExistenceId.HasValue)
            {
                TypeExistence = context.TypeExistence.FirstOrDefault(x => x.TypeExistenceId == model.TypeExistenceId);
                TypeExistence.State = ConstantHelpers.ESTADO.INACTIVO;
            }

            context.SaveChanges();
            PostMessage(MessageType.Success);

            return RedirectToAction("ListTypeExistence");
        }
        #endregion
        #region Supplier
        public ActionResult ListSupplier(Int32? Page, String BussinessName)
        {
            var model = new ListSupplierViewModel();
            model.Fill(CargarDatosContext(), Page, BussinessName);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult AddEditSupplier(Int32? SupplierId)
        {
            var model = new AddEditSupplierViewModel();
            model.Fill(CargarDatosContext(), SupplierId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditSupplier(AddEditSupplierViewModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    model.Fill(CargarDatosContext(), model.SupplierId);
                    TryUpdateModel(model);
                    PostMessage(MessageType.Error);
                    return View(model);
                }

                Supplier supplier = null;

                if (model.SupplierId.HasValue)
                {
                    supplier = context.Supplier.FirstOrDefault(x => x.SupplierId == model.SupplierId);
                }
                else
                {
                    supplier = new Supplier();
                    supplier.State = ConstantHelpers.ESTADO.ACTIVO;
                    context.Supplier.Add(supplier);
                }

                supplier.CountryId = model.CountryId;
                supplier.Code = model.Code;
                supplier.RUC = model.RUC;
                supplier.BussinessName = model.BussinessName;
                supplier.IsActive = model.IsActive;
                supplier.Provenance = model.Provenance;
                supplier.Origin = model.Origin;
                supplier.Address = model.Address;
                supplier.ZipCode = model.ZipCode;
                supplier.Retention = model.Retention;
                supplier.Detraction = model.Detraction;
                supplier.Perception = model.Perception;

                context.SaveChanges();
                PostMessage(MessageType.Success);

                return RedirectToAction("ListSupplier");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.SupplierId);

                return View(model);
            }
        }
        [EncryptedActionParameter]
        public ActionResult _DeleteSupplier(Int32? SupplierId)
        {
            var model = new _DeleteSupplierViewModel();
            model.Fill(CargarDatosContext(), SupplierId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteSupplier(_DeleteSupplierViewModel model)
        {
            Supplier supplier = null;

            if (model.SupplierId.HasValue)
            {
                supplier = context.Supplier.FirstOrDefault(x => x.SupplierId == model.SupplierId);
                supplier.State = ConstantHelpers.ESTADO.INACTIVO;
            }

            context.SaveChanges();
            PostMessage(MessageType.Success);

            return RedirectToAction("ListSupplier");
        }
        #endregion
        #region Item
        public ActionResult ListItem(Int32? Page, String Description)
        {
            var model = new ListItemViewModel();
            model.Fill(CargarDatosContext(), Page, Description);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult AddEditItem(Int32? ItemId)
        {
            var model = new AddEditItemViewModel();
            model.Fill(CargarDatosContext(), ItemId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditItem(AddEditItemViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Fill(CargarDatosContext(), model.ItemId);
                    TryUpdateModel(model);
                    PostMessage(MessageType.Error);
                    return View(model);
                }

                Item item = null;

                if (model.ItemId.HasValue)
                {
                    item = context.Item.FirstOrDefault(x => x.ItemId == model.ItemId);
                }
                else
                {
                    item = new Item();
                    item.State = ConstantHelpers.ESTADO.ACTIVO;
                    context.Item.Add(item);
                }

                item.Description = model.Description;

                context.SaveChanges();
                PostMessage(MessageType.Success);

                return RedirectToAction("ListItem");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.ItemId);

                return View(model);
            }
        }
        [EncryptedActionParameter]
        public ActionResult _DeleteItem(Int32? ItemId)
        {
            var model = new _DeleteItemViewModel();
            model.Fill(CargarDatosContext(), ItemId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteItem(_DeleteItemViewModel model)
        {
            Item item = null;

            if (model.ItemId.HasValue)
            {
                item = context.Item.FirstOrDefault(x => x.ItemId == model.ItemId);
                item.State = ConstantHelpers.ESTADO.INACTIVO;
            }

            context.SaveChanges();
            PostMessage(MessageType.Success);

            return RedirectToAction("ListItem");
        }
        #endregion
        #region Contact
        [EncryptedActionParameter]
        public ActionResult ListContact(Int32? SupplierId)
        {
            var model = new ListContactViewModel();
            model.Fill(CargarDatosContext(), SupplierId);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult _AddEditContact(Int32? ContactId, Int32 SupplierId)
        {
            var model = new _AddEditContactViewModel();
            model.Fill(CargarDatosContext(), ContactId, SupplierId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _AddEditContact(_AddEditContactViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Fill(CargarDatosContext(), model.ContactId, model.SupplierId);
                    TryUpdateModel(model);
                    PostMessage(MessageType.Error);
                    return View(model);
                }

                Contact contact = null;

                if (model.ContactId.HasValue)
                {
                    contact = context.Contact.FirstOrDefault(x => x.ContactId == model.ContactId);
                }
                else
                {
                    contact = new Contact();
                    contact.State = ConstantHelpers.ESTADO.ACTIVO;
                    context.Contact.Add(contact);
                }

                contact.SupplierId = model.SupplierId;
                contact.ItemId = model.ItemId;
                contact.Name = model.Name;
                contact.LastName = model.LastName;
                contact.SecondLastName = model.SecondLastName;
                contact.Phone = model.Phone;
                contact.Email = model.Email;

                context.SaveChanges();
                PostMessage(MessageType.Success);

                return RedirectToActionSecure("ListContact", "Master", new { SupplierId = model.SupplierId });
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.Fill(CargarDatosContext(), model.ContactId, model.SupplierId);

                return View(model);
            }
        }
        [EncryptedActionParameter]
        public ActionResult _DeleteContact(Int32? ContactId)
        {
            var model = new _DeleteContactViewModel();
            model.Fill(CargarDatosContext(), ContactId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteContact(_DeleteContactViewModel model)
        {
            Contact contact = null;

            if (model.ContactId.HasValue)
            {
                contact = context.Contact.FirstOrDefault(x => x.ContactId == model.ContactId);
                contact.State = ConstantHelpers.ESTADO.INACTIVO;
            }

            context.SaveChanges();
            PostMessage(MessageType.Success);

            return RedirectToActionSecure("ListContact", "Master", new { SupplierId = model.SupplierId });
        }
        #endregion
        #region BankContact
        [EncryptedActionParameter]
        public ActionResult _AddEditBankContact(Int32? ContactId)
        {
            var model = new _AddEditBankContactViewModel();
            //model.Fill(CargarDatosContext(), ContactId);
            return View(model);
        }
        #endregion
    }
}