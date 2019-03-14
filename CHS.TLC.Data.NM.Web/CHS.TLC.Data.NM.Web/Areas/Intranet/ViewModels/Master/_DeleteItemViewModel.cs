using CHS.TLC.Data.NM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet.ViewModels.Master
{
    public class _DeleteItemViewModel
    {
        public Int32? ItemId { get; set; }
        public String Description { get; set; }
        public _DeleteItemViewModel()
        {
        }
        public void Fill(CargarDatosContext c, Int32? itemId)
        {
            this.ItemId = itemId;

            if (this.ItemId.HasValue)
            {
                var item = c.context.Item.FirstOrDefault(x => x.ItemId == this.ItemId);

                this.Description = item.Description;
            }
        }
    }
}