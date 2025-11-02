using Application.Service;
using Microsoft.AspNetCore.Mvc;
using Contract.User.Response;
using System.Collections.Generic;

namespace Presentation.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("GetUser/{id}")]
    public ActionResult GetUserById([FromRoute]int id)
    {
        var user = _userService.GetUserById(id);
        return Ok(user);
    }

    [HttpGet("GetAllClients")]
    public ActionResult<List<UserResponse>> GetAllClients()
    {
        var users = _userService.GetAllClients();
        return Ok(users);
    }
    [HttpDelete("DeleteClient/{id}")]
    public ActionResult DeleteClient([FromRoute] int id)
    {
        var result = _userService.DeleteClient(id);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpPost]
    public ActionResult CreateUser([FromBody] Contract.User.Request.CreateUserRequest request)
    {
        var result = _userService.CreateUser(request);
        if (result)
        {
            return Ok();
        }
        return BadRequest("No se creo el Usuario");
    }

    [HttpPut("UpdateUser/{id}")]
    public ActionResult UpdateUser([FromRoute] int id, [FromBody] Contract.User.Request.UpdateUserRequest request)
    {
        var result = _userService.UpdateUser(id, request);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }
}

