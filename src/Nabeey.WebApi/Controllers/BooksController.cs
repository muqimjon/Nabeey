using Nabeey.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.DTOs.Books;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Configurations;
using Microsoft.AspNetCore.Authorization;

namespace Nabeey.Web.Controllers;

public class BooksController : BaseController
{
	private readonly IBookService bookService;
	public BooksController(IBookService bookService)
	{
		this.bookService = bookService;
	}

    [ProducesResponseType(typeof(BookResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost("create")]
	public async Task<IActionResult> PostAsync([FromQuery] BookCreationDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.bookService.AddAsync(dto)
		});

    [ProducesResponseType(typeof(BookResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("update")]
	public async Task<IActionResult> UpdateAsync([FromQuery] BookUpdateDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.bookService.ModifyAsync(dto)
		});

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("delete/{id:long}")]
	public async Task<IActionResult> DeleteAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.bookService.DeleteAsync(id)
		});

    [ProducesResponseType(typeof(BookResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
	[HttpGet("get/{id:long}")]
	public async Task<IActionResult> GetAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.bookService.RetrieveByIdAsync(id)
		});

    [ProducesResponseType(typeof(IEnumerable<BookResultDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
	[HttpGet("get-all")]
	public async ValueTask<IActionResult> GetAllAsync(
		[FromQuery] PaginationParams @params,
		string search)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.bookService.RetrieveAllAsync(@params, search)
		});


    [ProducesResponseType(typeof(IEnumerable<BookResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
	[HttpGet("get-by-categoryId/{categoryId:long}")]
	public async ValueTask<IActionResult> GetAllByCategoryIdAsync(long categoryId)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.bookService.RetrieveAllByCategoryIdAsync(categoryId)
		});
}
