using System.Web.Mvc;

namespace Examples.AddressBook.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}