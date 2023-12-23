using Nabeey.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Service.DTOs.Certificates;
using Microsoft.AspNetCore.Authorization;

namespace Nabeey.Web.Controllers;

public class QuizResultController : BaseController
{
	private readonly IQuizResultService quizResultService;
	private readonly ICertificateService certificateService;
    public QuizResultController(IQuizResultService quizResultService, ICertificateService certificateService)
    {
        this.quizResultService = quizResultService;
        this.certificateService = certificateService;
    }

    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [HttpGet("get-by-quizId-userId/{quizId:long}/{userId:long}")]
    public async ValueTask<IActionResult> GetAsync(long userId, long quizId)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.quizResultService.RetrieveByUserIdAsync(userId, quizId)
		});


    [ProducesResponseType(typeof(IEnumerable<ResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
	[HttpGet("get-by-quizId/{quizId:long}")]
	public async ValueTask<IActionResult> GetByQuizIdAsync(long quizId)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.quizResultService.RetrieveAllQuizIdAsync(quizId)
		});

    [ProducesResponseType(typeof(IEnumerable<CertificateResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [HttpGet("get-certificate/{quizId:long}")]
    public async ValueTask<IActionResult> GetCertificateAsync(long userId, long quizId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.certificateService.RetrieveByQuizIdCertificateAsync(userId, quizId)
        });
}