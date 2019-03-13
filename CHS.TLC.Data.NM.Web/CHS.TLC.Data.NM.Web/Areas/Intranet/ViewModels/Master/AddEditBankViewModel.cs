using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class AddEditBankViewModel
    {
        public Int32? BankId { get; set; }
        public String Description { get; set; }
        public String Type { get; set; }
        public String State { get; set; }
        public List<SelectListItem> LstBankType { get; set; } = new List<SelectListItem>();
        public AddEditBankViewModel()
        {
            this.LstBankType.Add(new SelectListItem { Value = ConstantHelpers.TIPOBANCO.NACIONAL, Text = "NACIONAL" });
            this.LstBankType.Add(new SelectListItem { Value = ConstantHelpers.TIPOBANCO.EXTRANJERO, Text = "EXTRANJERO" });
        }
        public void Fill(CargarDatosContext c, Int32? bankId)
        {
            this.BankId = bankId;

            if (this.BankId.HasValue)
            {
                var bank = c.context.Bank.FirstOrDefault(x => x.BankId == this.BankId);

                this.Description = bank.Description;
                this.Type = bank.Type;
                this.State = bank.State;
            }
        }
    }
}