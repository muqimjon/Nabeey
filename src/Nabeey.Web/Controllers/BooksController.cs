using Microsoft.AspNetCore.Mvc;

namespace Nabeey.Web.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
