using Application.Interfaces;
using Contract.PhotoSession.Request;
using Contract.PhotoSession.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotoSessionController : ControllerBase
{
    private readonly IPhotoSessionService _service;

    public PhotoSessionController(IPhotoSessionService service) => _service = service;

    [HttpGet("Get/{id}")]
    public ActionResult<PhotoSessionResponse> GetById([FromRoute] int id)
    {
        var resp = _service.GetById(id);
        if (resp == null) return NotFound();
        return Ok(resp);
    }

    [HttpGet("GetAll")]
    public ActionResult<List<PhotoSessionResponse>> GetAll()
    {
        var list = _service.GetAll();
        return Ok(list);
    }
    [Authorize(Roles = "Photographer")]

    [HttpPost]
    public IActionResult Create([FromBody] CreatePhotoSessionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var created = _service.CreatePhotoSession(request);
            if (created == null) return BadRequest("No se pudo crear la sesión.");
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return StatusCode(500, "Ocurrió un error interno al crear la sesión.");
        }
    }

    [Authorize(Roles = "Photographer")]

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdatePhotoSessionRequest request)
    {
        if (id != request.Id) return BadRequest("El id de ruta y el cuerpo no coinciden.");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var updated = _service.UpdatePhotoSession(request);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return StatusCode(500, "Ocurrió un error interno al actualizar la sesión.");
        }
    }

    [Authorize(Roles = "Photographer")]

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        try
        {
            var deleted = _service.DeletePhotoSession(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
        catch
        {
            return StatusCode(500, "Ocurrió un error interno al eliminar la sesión.");
        }
    }   
}