using Application.Abstraction;
using Application.Service;
using Contract.Location.Request;
using Contract.Location.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet("GetAllLocation")]
    public ActionResult<List<LocationResponse>> GetAllLocation()
    {
        var locations = _locationService.GetAllLocation();
        return Ok(locations);
    }

    [HttpGet("GetLocation/{id}")]
    public ActionResult<LocationResponse> GetLocation([FromRoute] string id)
    {
        var location = _locationService.GetLocation(id);
        if (location == null) return NotFound();
        return Ok(location);
    }

    [HttpPut("UpdateLocation/{id}")]
    public ActionResult UpdateLocation([FromRoute] int id, [FromBody] UpdateLocationRequest request)
    {
        var result = _locationService.UpdateLocation(id, request);
        if (result) return Ok();
        return NotFound();
    }

    [HttpDelete("DeleteLocation/{id}")]
    public ActionResult DeleteLocation([FromRoute] int id)
    {
        var result = _locationService.DeleteLocation(id);
        if (result) return Ok();
        return NotFound();
    }

    [HttpPost]
        public ActionResult CreateLocation([FromBody] CreateLocationRequest request)
    {
        var result = _locationService.CreateLocation(request);
        if (result) return Ok();
        return BadRequest("No se creo la Location");
    }
}
