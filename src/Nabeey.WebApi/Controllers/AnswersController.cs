using Nabeey.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.Service.DTOs.Answers;
using Nabeey.Domain.Configurations;
using Microsoft.AspNetCore.Authorization;
using Nabeey.Service.Exceptions;

namespace Nabeey.Web.Controllers;

public class AnswersController : BaseController
{
	private readonly IAnswerService answerService;
	public AnswersController(IAnswerService answerService)
	{
		this.answerService = answerService;
	}

    [ProducesResponseType(typeof(AnswerResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AlreadyExistException), StatusCodes.Status403Forbidden)]
    [HttpPost("create")]
	public async Task<IActionResult> PostAsync([FromQuery] AnswerCreationDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.answerService.AddAsync(dto)
		});

    [ProducesResponseType(typeof(AnswerResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    [HttpPut("update")]
	public async Task<IActionResult> UpdateAsync([FromQuery] AnswerUpdateDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.answerService.ModifyAsync(dto)
		});

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    [HttpDelete("delete/{id:long}")]
	public async Task<IActionResult> DeleteAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.answerService.RemoveAsync(id)
		});

    [ProducesResponseType(typeof(AnswerResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    [AllowAnonymous]
	[HttpGet("get/{id:long}")]
	public async Task<IActionResult> GetAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.answerService.RetrieveByIdAsync(id)
		});

    [ProducesResponseType(typeof(IEnumerable<AnswerResultDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
	[HttpGet("get-all")]
	public async ValueTask<IActionResult> GetAllAsync(
		[FromQuery] PaginationParams @params)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.answerService.RetrieveAllAsync(@params)
			});

    [ProducesResponseType(typeof(IEnumerable<AnswerResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    [AllowAnonymous]
	[HttpGet("get-by-questionId/{questionId:long}")]
	public async ValueTask<IActionResult> GetAllByContentIdAsync(long questionId)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.answerService.RetrieveAllByQuestionIdAsync(questionId)
		});
}
