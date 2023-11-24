using Microsoft.AspNetCore.Mvc;

namespace Nabeey.Web.Controllers
{
    public class CertificatesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
