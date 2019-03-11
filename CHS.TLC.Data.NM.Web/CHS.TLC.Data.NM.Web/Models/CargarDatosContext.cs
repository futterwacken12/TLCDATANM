using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Models
{
    public class CargarDatosContext
    {
        public dbcollectionagentEntities context { get; set; }
        public HttpSessionStateBase session { get; set; }
        public HttpContextBase httpContext { get; set; }
        public String currentLanguage { get; set; }
    }
}