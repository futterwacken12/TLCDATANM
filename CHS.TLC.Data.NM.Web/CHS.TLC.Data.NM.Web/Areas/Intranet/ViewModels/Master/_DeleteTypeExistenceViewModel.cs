using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class _DeleteTypeExistenceViewModel
    {
        public Int32? TypeExistenceId { get; set; }
        public String Description { get; set; }
        public _DeleteTypeExistenceViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? typeExistenceId)
        {
            this.TypeExistenceId = typeExistenceId;

            if (this.TypeExistenceId.HasValue)
            {
                var typeExistence = c.context.TypeExistence.FirstOrDefault(x => x.TypeExistenceId == this.TypeExistenceId);

                this.Description = typeExistence.Description;
            }
        }
    }
}