using Nabeey.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.ContentAudios;
using Microsoft.AspNetCore.Authorization;
using Nabeey.Service.Exceptions;

namespace Nabeey.Web.Controllers;

public class ContentAudiosController : BaseController
{
	private readonly IContentAudioService contentAudioService;
	public ContentAudiosController(IContentAudioService contentAudioService)
	{
		this.contentAudioService = contentAudioService;
	}

    [ProducesResponseType(typeof(ContentAudioResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AlreadyExistException), StatusCodes.Status403Forbidden)]
    [HttpPost("create")]
	public async ValueTask<IActionResult> PostAsync([FromForm] ContentAudioCreationDto dto)
	   => Ok(new Response
	   {
		   StatusCode = 200,
		   Message = "Success",
		   Data = await this.contentAudioService.AddAsync(dto)
	   });

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    [HttpDelete("delete/{id:long}")]
	public async ValueTask<IActionResult> DeleteAsync(long id)
	   => Ok(new Response
	   {
		   StatusCode = 200,
		   Message = "Success",
		   Data = await this.contentAudioService.RemoveAsync(id)
	   });

    [ProducesResponseType(typeof(ContentAudioResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    [AllowAnonymous]
	[HttpGet("get/{id:long}")]
	public async ValueTask<IActionResult> GetAsync(long id)
	  => Ok(new Response
	  {
		  StatusCode = 200,
		  Message = "Success",
		  Data = await this.contentAudioService.RetrieveByIdAsync(id)
	  });

    [ProducesResponseType(typeof(IEnumerable<ContentAudioResultDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
	[HttpGet("get-all")]
	public async ValueTask<IActionResult> GetAllAsync(
		[FromQuery] PaginationParams @params,
		[FromQuery] Filter filter, string search)
		  => Ok(new Response
		  {
			  StatusCode = 200,
			  Message = "Success",
			  Data = await this.contentAudioService.RetrieveAsync(@params, filter, search)
		  });

    [ProducesResponseType(typeof(IEnumerable<ContentAudioResultDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
	[HttpGet("get-by-categoryId/{categoryId:long}")]
	public async ValueTask<IActionResult> GetByCategoryIdAsync(long categoryId)
	  => Ok(new Response
	  {
		  StatusCode = 200,
		  Message = "Success",
		  Data = await this.contentAudioService.RetrieveAllByCategoryIdAsync(categoryId)
	  });
}