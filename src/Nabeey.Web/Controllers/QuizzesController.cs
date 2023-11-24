using Microsoft.AspNetCore.Mvc;

namespace Nabeey.Web.Controllers
{
    public class QuizzesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
