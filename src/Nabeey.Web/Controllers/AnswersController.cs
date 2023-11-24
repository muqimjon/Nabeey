using Microsoft.AspNetCore.Mvc;

namespace Nabeey.Web.Controllers;

public class AnswersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
