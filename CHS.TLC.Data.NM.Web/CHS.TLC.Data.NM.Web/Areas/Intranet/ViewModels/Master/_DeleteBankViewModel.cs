using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class _DeleteBankViewModel
    {
        public Int32? BankId { get; set; }
        public String Description { get; set; }
        public _DeleteBankViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? nationalBankId)
        {
            this.BankId = nationalBankId;

            if (this.BankId.HasValue)
            {
                var bank = c.context.Bank.FirstOrDefault(x => x.BankId == this.BankId);

                this.Description = bank.Description;
            }
        }
    }
}