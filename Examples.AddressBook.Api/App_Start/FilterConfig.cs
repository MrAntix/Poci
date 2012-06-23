using System.Web.Mvc;

namespace Examples.AddressBook.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ValidationActionFilterAttribute());
        }
    }
}