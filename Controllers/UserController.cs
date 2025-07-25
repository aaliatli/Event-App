using EventManagement.Data;
using EventManagement.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery { Id = id });
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<IActionResult> UserRegister(RegisterUserCommand command)
    {
        await _mediator.Send(command);
        return Ok("Kullanıcı kayıt edildi.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var token = await _mediator.Send(command);
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Mail veya şifre hatalı.");
        }
        return Ok(new { token });
    }

}