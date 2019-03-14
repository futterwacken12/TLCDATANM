using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class AddEditItemViewModel
    {
        public Int32? ItemId { get; set; }
        public String Description { get; set; }
        public String State { get; set; }
        public AddEditItemViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? itemId)
        {
            this.ItemId = itemId;

            if (this.ItemId.HasValue)
            {
                var item = c.context.Item.FirstOrDefault(x => x.ItemId == this.ItemId);

                this.Description = item.Description;
                this.State = item.State;
            }
        }
    }
}