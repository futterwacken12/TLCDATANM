﻿using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class AddEditMeasureUnitViewModel
    {
        public Int32? MeasureUnitId { get; set; }
        public String Description { get; set; }
        public String Acronym { get; set; }
        public String State { get; set; }
        public AddEditMeasureUnitViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? measureUnitId)
        {
            this.MeasureUnitId = measureUnitId;

            if (this.MeasureUnitId.HasValue)
            {
                var measureUnit = c.context.MeasureUnit.FirstOrDefault(x => x.MeasureUnitId == this.MeasureUnitId);

                this.Description = measureUnit.Description;
                this.Acronym = measureUnit.Acronym;
                this.State = measureUnit.State;
            }
        }
    }
}