using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Helpers
{
    public class AppWebViewPage<TModel> : WebViewPage<TModel>
    {
        public DataHelper<TModel> Data { get; set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            Data = new DataHelper<TModel>(this);
        }

        public override void Execute()
        {
        }
    }
}