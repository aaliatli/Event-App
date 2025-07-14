using EventManagement.Data;
using EventManagement.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;


[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IMediator _mediator;
    private static List<UserEvent> _userEvents = new();
    private const int Capacity = 50;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly EventDbContext _context;

    public EventController(IMediator mediator, IHttpClientFactory httpClientFactory, EventDbContext context)
    {
        _mediator = mediator;
        _httpClientFactory = httpClientFactory;
        _context = context;
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
        return Ok(await _mediator.Send(new GetEventByIdQuery { Id = id }));
    }
    
    [HttpGet("paged")]
    public async Task<IActionResult> GetAllPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetAllEventsPagedQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword){
        var result = await _mediator.Send(new SearchEventQuery(keyword));
        return Ok(result);
    }

    [HttpPost("register-to-event")]
    public async Task<IActionResult> RegisterToEvent(Guid userId, Guid eventId){
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"http://localhost:5020/api/User/{userId}");
        var user = await response.Content.ReadFromJsonAsync<User>();

        if(!response.IsSuccessStatusCode){
            return NotFound("Kullanıcı bulunamadı");
        }

        var eventEntity = await _mediator.Send(new GetEventByIdQuery {Id = eventId});
        if(eventEntity == null){
            return NotFound("Etkinlik bulunamadı.");
        }

        var userEventCount = await _context.UserEvents.CountAsync(ue => ue.EventId == eventId);
        if(userEventCount > eventEntity.Capacity){
            return BadRequest("Etkinlik kapasitesi dolmuş.");
        }

        if(eventEntity.AgeRestriction > 0 && user.Age< eventEntity.AgeRestriction){
            return BadRequest($"Bu etkinlik için minimum yaş sınırı: {eventEntity.AgeRestriction}");
        }

        var userEvent = new UserEvent{
            UserId = userId, 
            EventId = eventId
        };

        _context.UserEvents.Add(userEvent);
        await _context.SaveChangesAsync();

        return Ok("Etkinliğe kayıt olundu. ");
    }

}