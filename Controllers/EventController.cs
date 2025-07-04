using EventManagement.Data;
using EventManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly EventDbContext _context;

    public EventController(EventDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateEvent(string Title, string Location, DateTime StartDate, DateTime EndDate, int Capacity)
    {

        if (string.IsNullOrWhiteSpace(Title) || StartDate == DateTime.MinValue || EndDate == DateTime.MinValue)
        {
            return BadRequest("Alanlar boş bırakılamaz.");
        }
        if (EndDate <= StartDate)
        {
            return BadRequest("Bitiş tarihi başlangıç tarihinden önce olamaz!");
        }
        if (Capacity <= 0)
        {
            return BadRequest("Kişi sayısı 0'dan küçük olamaz!");
        }
        if (StartDate < DateTime.Today)
        {
            return BadRequest("Başlangıç tarihi geçmiş bir tarih olamaz. ");
        }

        var newEvent = new Event
        {
            id = Guid.NewGuid(),
            Title = Title,
            Location = Location,
            StartDate = StartDate,
            EndDate = EndDate,
            Capacity = Capacity
        };

        _context.Events.Add(newEvent);
        _context.SaveChanges();


        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEvents(Guid id, string Title, string Location, DateTime StartDate, DateTime EndDate, int Capacity)
    {
        var existingEvent = _context.Events.FirstOrDefault(e => e.id == id);

        if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Location) || StartDate == DateTime.MinValue || EndDate == DateTime.MinValue)
        {
            return BadRequest("Alanlar boş bırakılamaz.");
        }
        if (existingEvent == null)
        {
            return BadRequest("Etkinlik bulunamadı.");
        }

        if (EndDate < StartDate)
        {
            return BadRequest("Bitiş tarihi başlangıç tarihinden önce olamaz!");
        }
        if (Capacity <= 0)
        {
            return BadRequest("Kişi sayısı 0'dan küçük olamaz!");
        }
        if (StartDate < DateTime.Today)
        {
            return BadRequest("Başlangıç tarihi geçmiş bir tarih olamaz. ");
        }

        existingEvent.Title = Title;
        existingEvent.Location = Location;
        existingEvent.StartDate = StartDate;
        existingEvent.EndDate = EndDate;
        existingEvent.Capacity = Capacity;

        _context.SaveChanges();
        return Ok();
    }

    [HttpGet("all")]
    public IActionResult GetAllEvents()
    {
        var listAll = _context.Events.ToList();
        return Ok(listAll);
    }

    [HttpGet("current")]
    public IActionResult GetCurrentEvents()
    {
        var dates = _context.Events.Where(e => e.StartDate >= DateTime.Today).Select(e => e.Title).ToList();
        return Ok(dates);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEvents(Guid id)
    {
        var deleteEvent = _context.Events.Where(e => e.id == id).ExecuteDelete();
        if (deleteEvent == 0)
        {
            return NotFound("Silinecek etkinlik yok.");
        }
        return Ok("Etkinlik başarıyla silindi.");
    }

}