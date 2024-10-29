using Microsoft.AspNetCore.Mvc;

namespace ProjectCatalog.Controllers
{
    public class BackendController : Controller
    {
        public IActionResult Index()
        {
            return View("Views/Dashboard.cshtml");
        }
    }
}