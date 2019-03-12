using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class _DeleteSubFamilyViewModel
    {
        public Int32? SubFamilyId { get; set; }
        public String Description { get; set; }
        public _DeleteSubFamilyViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? subFamilyId)
        {
            this.SubFamilyId = subFamilyId;

            if (this.SubFamilyId.HasValue)
            {
                var subFamily = c.context.SubFamily.FirstOrDefault(x => x.SubFamilyId == this.SubFamilyId);

                this.Description = subFamily.Description;
            }
        }
    }
}