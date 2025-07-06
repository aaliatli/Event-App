using EventManagement.Data;
using EventManagement.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateEventCommand command)
    {
        await _mediator.Send(command);
        return Ok("Etkinlik başarıyla oluşturuldu.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteEventCommand { Id = id });
        return Ok("Etkinlik silindi.");
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllEventsQuery()));
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent()
    {
        return Ok(await _mediator.Send(new GetCurrentEventsQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _mediator.Send(new GetEventByIdQuery{Id = id}));
    }
    

}