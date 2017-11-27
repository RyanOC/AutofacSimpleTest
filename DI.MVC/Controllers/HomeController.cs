using System.Web.Mvc;
using DI.Core.Abstractions;

namespace DI.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommerceEngine _commerceEngine;

        public HomeController(ICommerceEngine commerceEngine)
        {
            _commerceEngine = commerceEngine;
        }

        public ActionResult Index()
        {
            _commerceEngine.ProcessOrder();

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
