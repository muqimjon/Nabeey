using Nabeey.Web.Models;
using Nabeey.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.DTOs.Users;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Certificates;
using Microsoft.AspNetCore.Authorization;

namespace Nabeey.Web.Controllers;

public class UserController : BaseController
{
	private readonly IUserService userService;
	private readonly ICertificateService certificateService;
    public UserController(IUserService userService, ICertificateService certificateService, IWebHostEnvironment webHostEnvironment)
    {
        this.userService = userService;
        this.certificateService = certificateService;
    }

    [ProducesResponseType(typeof(QuizResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [AllowAnonymous]
	[HttpPost("create")]
	public async ValueTask<IActionResult> PostAsync([FromForm] UserCreationDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.userService.AddAsync(dto)
		});

    [ProducesResponseType(typeof(QuizResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("update")]
	public async ValueTask<IActionResult> PutAsync([FromForm] UserUpdateDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.userService.ModifyAsync(dto)
		});

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("delete/{id:long}")]
	public async ValueTask<IActionResult> DeleteAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.userService.RemoveAsync(id)
		});

    [ProducesResponseType(typeof(QuizResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
	[HttpGet("get/{id:long}")]
	public async ValueTask<IActionResult> GetByIdAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.userService.RetrieveByIdAsync(id)
		});

    [ProducesResponseType(typeof(IEnumerable<QuizResultDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
	[HttpGet("get-all")]
	public async ValueTask<IActionResult> GetAllAsync(
		[FromQuery] PaginationParams @params,
		[FromQuery] string search)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.userService.RetrieveAllAsync(@params, search)
		});

    [ProducesResponseType(typeof(QuizResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPatch("upgrade-role")]
	public async ValueTask<IActionResult> UpgradeRoleAsync(long id, Role role)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.userService.UpgradeRoleAsync(id, role)
		});


    [ProducesResponseType(typeof(IEnumerable<CertificateResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-certificate")]
	public async ValueTask<IActionResult> GetCertificate(long userId)
     => Ok(new Response
     {
         StatusCode = 200,
         Message = "Success",
         Data = await certificateService.RetriveUserCertificatesAsync(userId)
     });
}
