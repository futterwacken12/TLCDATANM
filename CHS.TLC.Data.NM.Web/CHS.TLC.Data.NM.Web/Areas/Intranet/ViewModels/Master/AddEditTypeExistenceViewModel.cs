using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class AddEditTypeExistenceViewModel
    {
        public Int32? TypeExistenceId { get; set; }
        public String Description { get; set; }
        public String State { get; set; }
        public Int32? FatherId { get; set; }
        public AddEditTypeExistenceViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? typeExistenceId, Int32? fatherid)
        {
            this.TypeExistenceId = typeExistenceId;
            this.FatherId = fatherid;

            if (this.TypeExistenceId.HasValue)
            {
                var typeExistence = c.context.TypeExistence.FirstOrDefault(x => x.TypeExistenceId == this.TypeExistenceId);

                this.Description = typeExistence.Description;
                this.State = typeExistence.State;
            }
        }
    }
}