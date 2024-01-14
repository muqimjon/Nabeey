using Nabeey.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.Service.DTOs.Certificates;
using Microsoft.AspNetCore.Authorization;
using Nabeey.Service.Exceptions;

namespace Nabeey.Web.Controllers;

public class CertificatesController : BaseController
{
    private readonly ICertificateService certificateService;
    public CertificatesController(ICertificateService certificateService)
    {
        this.certificateService = certificateService;
    }

    [ProducesResponseType(typeof(CertificateResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [HttpPost("generate")]
    public async ValueTask<IActionResult> Create(CertificateCreationDto dto)
     => Ok(new Response
     {
         StatusCode = 200,
         Message = "Success",
         Data = await certificateService.GenerateAsync(dto)
     });

    [ProducesResponseType(typeof(CertificateResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [HttpGet("get-by-id/{id:long}")]
    public async ValueTask<IActionResult> GetById(long id)
     => Ok(new Response
     {
         StatusCode = 200,
         Message = "Success",
         Data = await certificateService.RetrieveByIdAsync(id)
     });

    [ProducesResponseType(typeof(IEnumerable<CertificateResultDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetCertificate()
     => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await certificateService.RetrieveAllAsync()
        });
}
