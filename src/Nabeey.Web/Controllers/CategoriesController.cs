using Microsoft.AspNetCore.Mvc;

namespace Nabeey.Web.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
