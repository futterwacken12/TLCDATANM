using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Home
{
    public class MainOptionViewModel
    {
        public Int32? FatherId { get; set; }
        public List<Option> LstOption { get; set; }
        public List<String> LstGroupIcon { get; set; }
        public MainOptionViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? fatherId)
        {
            this.FatherId = fatherId;

            if (this.FatherId.HasValue)
            {
                this.LstOption = c.context.Option.Where(x => x.FatherId == this.FatherId && x.IsVisible).OrderBy(x => x.Order).ToList();
                this.LstGroupIcon = LstOption.Select(x => x.Icon).Distinct().ToList();
            }
        }
    }
}