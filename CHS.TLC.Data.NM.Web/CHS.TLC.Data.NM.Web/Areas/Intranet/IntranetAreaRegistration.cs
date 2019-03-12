﻿using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web.Areas.Intranet
{
    public class IntranetAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Intranet";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Intranet_default",
                "Intranet/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}