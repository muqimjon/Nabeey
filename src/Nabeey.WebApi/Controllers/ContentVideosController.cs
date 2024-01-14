using Nabeey.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.ContentVideos;
using Microsoft.AspNetCore.Authorization;
using Nabeey.Service.Exceptions;

namespace Nabeey.Web.Controllers;

public class ContentVideosController : BaseController
{
	private readonly IContentVideoService contentVideoService;
	public ContentVideosController(IContentVideoService contentVideoService)
	{
		this.contentVideoService = contentVideoService;
	}

    [ProducesResponseType(typeof(ContentVideoResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AlreadyExistException), StatusCodes.Status403Forbidden)]
    [HttpPost("create")]
	public async Task<IActionResult> PostAsync(ContentVideoCreationDto dto)
		   => Ok(new Response
		   {
			   StatusCode = 200,
			   Message = "Success",
			   Data = await this.contentVideoService.AddAsync(dto)
		   });

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    [HttpDelete("delete/{id:long}")]
	public async Task<IActionResult> DeleteAsync(long id)
	   => Ok(new Response
	   {
		   StatusCode = 200,
		   Message = "Success",
		   Data = await this.contentVideoService.RemoveAsync(id)
	   });

    [ProducesResponseType(typeof(ContentVideoResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    [AllowAnonymous]
	[HttpGet("get/{id:long}")]
	public async Task<IActionResult> GetAsync(long id)
	  => Ok(new Response
	  {
		  StatusCode = 200,
		  Message = "Success",
		  Data = await this.contentVideoService.RetrieveByIdAsync(id)
	  });

    [ProducesResponseType(typeof(IEnumerable<ContentVideoResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    [AllowAnonymous]
	[HttpGet("get-by-categoryId/{categorytId:long}")]
	public async Task<IActionResult> GetByCategoryIdAsync(long categorytId)
	  => Ok(new Response
	  {
		  StatusCode = 200,
		  Message = "Success",
		  Data = await this.contentVideoService.RetrieveAllByCategoryIdAsync(categorytId)
	  });

    [ProducesResponseType(typeof(IEnumerable<ContentVideoResultDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
	[HttpGet("get-all")]
	public async ValueTask<IActionResult> GetAllAsync(
		[FromQuery] PaginationParams @params,
		[FromQuery] Filter filter, string search)
	  => Ok(new Response
	  {
		  StatusCode = 200,
		  Message = "Success",
		  Data = await this.contentVideoService.RetrieveAsync(@params, filter, search)
	  });
}