using Nabeey.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Configurations;
using Microsoft.AspNetCore.Authorization;
using Nabeey.Service.DTOs.ContentCategories;

namespace Nabeey.Web.Controllers;

public class ContentCategoriesController : BaseController
{
	private readonly IContentCategoryService contentCategoryService;
	public ContentCategoriesController(IContentCategoryService contentCategoryService)
	{
		this.contentCategoryService = contentCategoryService;
	}

    [ProducesResponseType(typeof(ContentCategoryResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost("create")]
	public async ValueTask<IActionResult> PostAsync([FromForm] ContentCategoryCreationDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.contentCategoryService.AddAsync(dto)
		});

    [ProducesResponseType(typeof(ContentCategoryResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("update")]
	public async ValueTask<IActionResult> PutAsync([FromForm] ContentCategoryUpdateDto dto)
	   => Ok(new Response
	   {
		   StatusCode = 200,
		   Message = "Success",
		   Data = await this.contentCategoryService.ModifyAsync(dto)
	   });

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("delete/{id:long}")]
	public async ValueTask<IActionResult> DeleteAsync(long id)
	   => Ok(new Response
	   {
		   StatusCode = 200,
		   Message = "Success",
		   Data = await this.contentCategoryService.RemoveAsync(id)
	   });

    [ProducesResponseType(typeof(ContentCategoryResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
	[HttpGet("get/{id:long}")]
	public async ValueTask<IActionResult> GetByIdAsync(long id)
	   => Ok(new Response
	   {
		   StatusCode = 200,
		   Message = "Success",
		   Data = await this.contentCategoryService.RetrieveByIdAsync(id)
	   });

    [ProducesResponseType(typeof(IEnumerable<ContentCategoryResultDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
	[HttpGet("get-all")]
	public async ValueTask<IActionResult> GetAllAsync(
	[FromQuery] PaginationParams @params,
	[FromQuery] string search)
	=> Ok(new Response
	{
		StatusCode = 200,
		Message = "Success",
		Data = await this.contentCategoryService.RetrieveAllAsync(@params, search)
	});
}
