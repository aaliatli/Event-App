using EventManagement.Data;
using EventManagement.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase{

    private readonly IMediator _mediator;

    public UserController(IMediator mediator){
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id){
        var user = await _mediator.Send(new GetUserByIdQuery {Id = id});
        if(user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost("add-user")]
    public async Task<IActionResult> UserRegister(CreateUserCommand command){
        await _mediator.Send(command);
        return Ok("Kullanıcı kayıt edildi.");
    }

}