using Microsoft.AspNetCore.Mvc;

namespace Nabeey.Web.Controllers
{
    public class AudiosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
