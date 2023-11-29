using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;

namespace Nabeey.Web.Controllers;

public class AnswersController : Controller
{
    private readonly IAnswerService answerService;
    public AnswersController(IAnswerService answerService)
    {
        this.answerService = answerService;
    }

    public async ValueTask<IActionResult> Index()
    =>  View(await answerService.RetrieveAllAsync(
        new Domain.Configurations.PaginationParams
        {
            PageIndex = 1,
            PageSize = 10
        }));
}
