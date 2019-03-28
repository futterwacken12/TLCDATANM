using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class AddEditTaxPercentageViewModel
    {
        public Int32? TaxPercentageId { get; set; }
        public Decimal Description { get; set; }
        public String State { get; set; }
        public Int32? FatherId { get; set; }
        public AddEditTaxPercentageViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? taxPercentageId, Int32? fatherid)
        {
            this.TaxPercentageId = taxPercentageId;
            this.FatherId = fatherid;

            if (this.TaxPercentageId.HasValue)
            {
                var taxPercentage = c.context.TaxPercentage.FirstOrDefault(x => x.TaxPercentageId == this.TaxPercentageId);

                this.Description = taxPercentage.Description;
                this.State = taxPercentage.State;
            }
        }
    }
}