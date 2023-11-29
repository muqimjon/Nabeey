using Nabeey.Service.DTOs.Users;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Configurations;

namespace Nabeey.Web.Controllers;

public class UsersController : Controller
{
    private readonly IUserService userService;
    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    public async ValueTask<IActionResult> Index()
        => View(await userService.RetrieveAllAsync(
            new PaginationParams
            {
                PageIndex = 1,
                PageSize = 10
            }));

    [HttpGet("Index")]
    public async ValueTask<IActionResult> Index(int index)
    => View(await userService.RetrieveAllAsync(
        new PaginationParams
        {
            PageIndex = index,
            PageSize = 10
        }));

    public async ValueTask<IActionResult> Details(long id)
        => View(await userService.RetrieveByIdAsync(id));

    public async ValueTask<IActionResult> Update(long id)
        => View(await userService.RetrieveByIdAsync(id));

    [HttpPut]
    public async ValueTask<IActionResult> Update(UserUpdateDto dto)
    {
        await userService.ModifyAsync(dto);
        return Redirect("index");
    }

    [HttpPost]
    public async ValueTask<IActionResult> Delete(long id)
    {
        var i = id;
        await userService.RemoveAsync(id);
        return Redirect("/");
    }

    public IActionResult Create()
        => View();

    [HttpPost]
    public async ValueTask<IActionResult> Create(UserCreationDto dto)
    {
        await userService.AddAsync(dto);
        return Redirect("index");
    }

}
