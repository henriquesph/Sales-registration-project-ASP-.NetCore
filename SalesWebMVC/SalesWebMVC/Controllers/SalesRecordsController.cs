using Microsoft.AspNetCore.Mvc;

namespace SalesWebMVC.Controllers
{
    public class SalesRecordsController : Controller  // mesmo nome que o model - o caminho para chegar na página está no menu (_Latout.cshtml)
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleSearch()
        {
            return View();
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
