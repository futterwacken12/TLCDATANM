using System.Web;
using System.Web.Mvc;

namespace CHS.TLC.Data.NM.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
