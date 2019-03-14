using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class _AddEditBankContactViewModel
    {
        public Int32 ContactId { get; set; }
        public Int32 BankId { get; set; }
        public String AccountSoles { get; set; }
        public String AccountDollars { get; set; }
        public String State { get; set; }
        public _AddEditBankContactViewModel()
        {
        }
    }
}