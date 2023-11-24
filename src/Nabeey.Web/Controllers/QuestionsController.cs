using Microsoft.AspNetCore.Mvc;

namespace Nabeey.Web.Controllers
{
    public class QuestionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
