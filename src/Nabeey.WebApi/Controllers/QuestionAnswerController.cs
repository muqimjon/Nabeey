using Nabeey.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.Service.DTOs.QuestionAnswers;

namespace Nabeey.Web.Controllers;

public class QuestionAnswerController : BaseController
{
	private readonly IQuestionAnswerService service;
	public QuestionAnswerController(IQuestionAnswerService service)
	{
		this.service = service;
	}

    [ProducesResponseType(typeof(QuestionAnswerResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost("create")]
	public async ValueTask<IActionResult> PostAsync(QuestionAnswerCreationDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.service.AddAsync(dto)
		});

    [ProducesResponseType(typeof(QuestionAnswerResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("update")]
	public async ValueTask<IActionResult> UpdateAsync(QuestionAnswerUpdateDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.service.ModifyAsync(dto)
		});
}