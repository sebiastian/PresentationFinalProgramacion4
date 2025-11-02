using Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotographerController : ControllerBase
{
    private readonly IPhotographerService _photographerService;

    public PhotographerController(IPhotographerService photographerService)
    {
        _photographerService = photographerService;
    }

    [HttpGet("GetPhotographer/{id}")]
    public ActionResult GetPhotographerById([FromRoute] int id)
    {
        var photographer = _photographerService.GetPhotographerById(id);
        return Ok(photographer);
    }
}