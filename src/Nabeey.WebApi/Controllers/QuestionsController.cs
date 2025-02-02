﻿using Nabeey.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Questions;
using Microsoft.AspNetCore.Authorization;
using Nabeey.Service.Exceptions;

namespace Nabeey.Web.Controllers;

public class QuestionsController : BaseController
{
	private readonly IQuestionService questionService;
	public QuestionsController(IQuestionService questionService)
	{
		this.questionService = questionService;
	}

    [ProducesResponseType(typeof(QuestionResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AlreadyExistException), StatusCodes.Status403Forbidden)]
    [HttpPost("create")]
	public async ValueTask<IActionResult> PostAsync([FromForm] QuestionCreationDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.questionService.AddAsync(dto)
		});

    [ProducesResponseType(typeof(QuestionResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    [HttpPut("update")]
	public async ValueTask<IActionResult> UpdateAsync([FromForm] QuestionUpdateDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.questionService.ModifyAsync(dto)
		});

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    [HttpDelete("delete/{id:long}")]
	public async ValueTask<IActionResult> DeleteAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.questionService.RemoveAsync(id)
		});

    [ProducesResponseType(typeof(QuestionResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    [AllowAnonymous]
	[HttpGet("get/{id:long}")]
	public async ValueTask<IActionResult> GetAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.questionService.RetrieveByIdAsync(id)
		});

    [ProducesResponseType(typeof(IEnumerable<QuestionResultDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
	[HttpGet("get-all")]
	public async ValueTask<IActionResult> GetAllAsync(
		[FromQuery] PaginationParams @params,
		[FromQuery] Filter filter, string search)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.questionService.RetrieveAllAsync(@params, filter, search)
		});
}
