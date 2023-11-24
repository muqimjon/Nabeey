using Microsoft.AspNetCore.Mvc;

namespace Nabeey.Web.Controllers
{
    public class VideosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
