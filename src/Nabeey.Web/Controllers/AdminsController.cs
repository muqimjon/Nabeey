using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;

namespace Nabeey.Web.Controllers;

public class AdminsController : Controller
{
    private readonly IUserService userService;
    public AdminsController(IUserService userService)
    {
        this.userService = userService;
    }

    public async ValueTask<IActionResult> Index()
        => View(await userService.RetrieveAllAsync());
}
