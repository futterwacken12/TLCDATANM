using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class AddEditFamilyViewModel
    {
        public Int32? FamilyId { get; set; }
        public String Description { get; set; }
        public String SKUCode { get; set; }
        public String State { get; set; }
        public Int32? FatherId { get; set; }
        public AddEditFamilyViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? familyId, Int32? fatherId)
        {
            this.FamilyId = familyId;
            this.FatherId = fatherId;

            if (this.FamilyId.HasValue)
            {
                var family = c.context.Family.FirstOrDefault(x => x.FamilyId == this.FamilyId);

                this.Description = family.Description;
                this.SKUCode = family.SKUCode;
                this.State = family.State;
            }
        }
    }
}