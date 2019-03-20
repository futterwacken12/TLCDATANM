using CHS.TLC.Data.NM.Web.Models;
using CHS.TLC.Data.NM.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Home
{
    public class MainOptionViewModel
    {
        public Int32? FatherId { get; set; }
        public List<RoleOption> LstRoleOption { get; set; }
        public List<String> LstGroupIcon { get; set; }
        public MainOptionViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? fatherId)
        {
            this.FatherId = fatherId;

            if (this.FatherId.HasValue)
            {
                var roleId = c.session.GetRolId();
                this.LstRoleOption = c.context.RoleOption.Where(x => x.RoleId == roleId && x.Option.FatherId == this.FatherId && x.Option.IsVisible == true).OrderBy(x => x.Option.Order).ToList();
                this.LstGroupIcon = LstRoleOption.Select(x => x.Option.Icon).Distinct().ToList();
            }
        }
    }
}