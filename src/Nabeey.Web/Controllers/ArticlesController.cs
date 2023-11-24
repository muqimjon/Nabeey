using Microsoft.AspNetCore.Mvc;

namespace Nabeey.Web.Controllers
{
    public class ArticlesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
