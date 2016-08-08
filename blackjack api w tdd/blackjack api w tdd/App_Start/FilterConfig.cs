using System.Web;
using System.Web.Mvc;

namespace blackjack_api_w_tdd
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
