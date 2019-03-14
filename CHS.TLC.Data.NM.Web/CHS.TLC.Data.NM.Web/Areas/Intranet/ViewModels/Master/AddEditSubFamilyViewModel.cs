using CHS.TLC.Data.NM.Web.Helpers;
using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class AddEditSubFamilyViewModel
    {
        public Int32? SubFamilyId { get; set; }
        public Int32 FamilyId { get; set; }
        public String Description { get; set; }
        public String State { get; set; }
        public List<Family> LstFamily { get; set; } = new List<Family>();
        public AddEditSubFamilyViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? subFamilyId)
        {
            this.SubFamilyId = subFamilyId;
            this.LstFamily = c.context.Family.Where(x => x.State == ConstantHelpers.ESTADO.ACTIVO).ToList();

            if (this.SubFamilyId.HasValue)
            {
                var subFamily = c.context.SubFamily.FirstOrDefault(x => x.SubFamilyId == this.SubFamilyId);

                this.Description = subFamily.Description;
                this.FamilyId = subFamily.FamilyId;
                this.State = subFamily.State;
            }
        }
    }
}