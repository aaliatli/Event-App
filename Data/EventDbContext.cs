using EventManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Data;

public class EventDbContext : DbContext
{
    public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }
    public DbSet<Event> Events { get; set;}
}