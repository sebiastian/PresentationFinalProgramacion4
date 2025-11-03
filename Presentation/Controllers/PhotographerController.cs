using Application.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Contract.Photographer.Response;
using Contract.Photographer.Request;

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
    public ActionResult<PhotographerResponse> GetPhotographerById([FromRoute] int id)
    {
        var photographer = _photographerService.GetPhotographerById(id);
        if (photographer == null)
            return NotFound();
        return Ok(photographer);
    }

    [HttpGet("GetAllPhotographers")]
    public ActionResult<List<PhotographerResponse>> GetAllPhotographers()
    {
        var photographers = _photographerService.GetAllPhotographers();
        return Ok(photographers);
    }

    [HttpPost]
    public ActionResult CreatePhotographer([FromBody] CreatePhotographerRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = _photographerService.CreatePhotographer(request);
        if (result) return Ok();
        return BadRequest("No se pudo crear el fotógrafo");
    }

    [HttpPut("UpdatePhotographer/{id}")]
    public ActionResult UpdatePhotographer([FromRoute] int id, [FromBody] UpdatePhotographerRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = _photographerService.UpdatePhotographer(id, request);
        if (result) return Ok();
        return NotFound();
    }

    [HttpDelete("DeletePhotographer/{id}")]
    public ActionResult DeletePhotographer([FromRoute] int id)
    {
        var result = _photographerService.DeletePhotographer(id);
        if (result) return Ok();
        return NotFound();
    }
}