using Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotoSessionController : ControllerBase
{
    private readonly IPhotoSessionService _service;

    public PhotoSessionController(IPhotoSessionService service) => _service = service;

    [HttpGet("Get/{id}")]
    public ActionResult GetById([FromRoute] int id)
    {
        var resp = _service.GetById(id);
        if (resp == null) return NotFound();
        return Ok(resp);
    }
}